using Hangfire;
using Hangfire.MySql.Core;
using JuCheap.Core.Data;
using JuCheap.Core.Infrastructure.Utilities;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Services;
using JuCheap.Core.Web.Filters;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace JuCheap.Core.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var repository = LogManager.CreateRepository(Constants.Log4net.RepositoryName);
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.ExpireTimeSpan = TimeSpan.FromMinutes(480);//cookie默认有效时间为8个小时
                    o.LoginPath = new PathString("/Home/Login");
                    o.LogoutPath = new PathString("/Home/Logout");
                    o.Cookie = new CookieBuilder
                    {
                        HttpOnly = true,
                        Name = ".JuCheap.Core.Identity",
                        Path = "/"
                    };
                    //o.DataProtectionProvider = null;//如果需要做负载均衡，就需要提供一个Key
                });
            //使用Sql Server数据库
            services.AddDbContext<JuCheapContext>(options =>
            {
                var loggerFactory = new LoggerFactory();
                loggerFactory.AddProvider(new EFLoggerProvider());
                options.UseLoggerFactory(loggerFactory)
                        .UseSqlServer(Configuration.GetConnectionString("Connection_SqlServer"));
            });

            ////使用Sqlite数据库
            //services.AddDbContext<JuCheapContext>(options => options.UseSqlite(Configuration.GetConnectionString("Connection_Sqlite")));

            //使用MySql数据库
            //services.AddDbContext<JuCheapContext>(options => options.UseMySql(Configuration.GetConnectionString("Connection_MySql")));

            //权限验证filter
            services.AddMvc(cfg =>
            {
                cfg.Filters.Add(new RightFilter());
            }).AddJsonOptions(option =>
            {
                option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            //.AddJsonOptions(option => option.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());//配置大小写问题，默认是首字母小写

            // service依赖注入
            services.UseJuCheapService();

            //hangfire自动任务配置数据库配置
            //使用sql server数据库做hangfire的持久化
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("Connection_Job_SqlServer")));

            //使用mysql数据库做hangfire的持久化
            //使用mysql的时候，由于很同学不知道怎么设置数据库和数据表的编码格式，会导致hangfire初始化失败，
            //所以不自动创建hangfire的数据表，需要手动导入根目录下Hangfire/hangfire.job.mysql.sql文件来创建表
            //var mySqlOption = new MySqlStorageOptions { PrepareSchemaIfNecessary = false };
            //services.AddHangfire(x => x.UseStorage(new MySqlStorage(Configuration.GetConnectionString("Connection_Job_MySql"),mySqlOption)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            //全局身份认证
            app.UseAuthentication();
            //访问记录middleware
            app.UseMiddleware<VisitMiddleware>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //初始化数据库以及初始数据
            Task.Run(async () =>
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var menues = MenuHelper.GetMenues();
                    var dbService = scope.ServiceProvider.GetService<IDatabaseInitService>();
                    await dbService.InitAsync(menues);
                }
            });

            //hangfire自动任务配置
            var jobOptions = new BackgroundJobServerOptions
            {
                ServerName = Environment.MachineName,
                WorkerCount = 1
            };
            app.UseHangfireServer(jobOptions);
            var option = new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            };
            app.UseHangfireDashboard("/task", option);
            //添加一个每天自动在凌晨的时候执行的统计任务
            RecurringJob.AddOrUpdate<ISiteViewService>(x => x.AddOrUpdate(), Cron.Daily());
            RecurringJob.AddOrUpdate(() => Console.WriteLine($"Job在{DateTime.Now}执行完成."), Cron.Minutely());
        }
    }

    public class EFLogger : ILogger
    {
        private readonly string categoryName;

        public EFLogger(string categoryName) => this.categoryName = categoryName;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            //ef core执行数据库查询时的categoryName为Microsoft.EntityFrameworkCore.Database.Command,日志级别为Information
            if (categoryName == "Microsoft.EntityFrameworkCore.Database.Command"
                    && logLevel == LogLevel.Information)
            {
                
            }
            var logContent = formatter(state, exception);
            //TODO: 拿到日志内容想怎么玩就怎么玩吧
            Debug.WriteLine(logContent);
        }

        public IDisposable BeginScope<TState>(TState state) => null;
    }

    public class EFLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName) => new EFLogger(categoryName);
        public void Dispose() { }
    }

    /// <summary>
    /// IIdentity扩展
    /// </summary>
    public static class IdentityExtention
    {
        /// <summary>
        /// 获取登录的用户ID
        /// </summary>
        /// <param name="identity">IIdentity</param>
        /// <returns></returns>
        public static string GetLoginUserId(this IIdentity identity)
        {
            var claim = (identity as ClaimsIdentity)?.FindFirst("LoginUserId");
            if (claim != null)
            {
                return claim.Value;
            }
            return string.Empty;
        }
    }
}
