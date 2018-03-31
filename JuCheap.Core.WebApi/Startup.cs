using System.Security.Claims;
using System.Security.Principal;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using JuCheap.Core.Data;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Services;
using JuCheap.Core.Services.AppServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace JuCheap.Core.WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // 配置IdentityServer
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResourceResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients());//添加授权客户端
            services.AddScoped<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddScoped<IProfileService, ProfileService>();

            //使用Sql Server数据库
            services.AddDbContext<JuCheapContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Connection_SqlServer")));
            //支持sql2008的row_number分页函数
            //services.AddDbContext<JuCheapContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Connection_SqlServer"), x => x.UseRowNumberForPaging()));

            ////使用Sqlite数据库
            //services.AddDbContext<JuCheapContext>(options => options.UseSqlite(Configuration.GetConnectionString("Connection_Sqlite")));

            //使用MySql数据库
            //services.AddDbContext<JuCheapContext>(options => options.UseMySql(Configuration.GetConnectionString("Connection_MySql")));

            // Add application services.
            // 1.automapper
            services.AddScoped<AutoMapper.IConfigurationProvider>(_ => AutoMapperConfig.GetMapperConfiguration());
            services.AddScoped(_ => AutoMapperConfig.GetMapperConfiguration().CreateMapper());

            // 2.service
            services.AddScoped<IDatabaseInitService, DatabaseInitService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IPathCodeService, PathCodeService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IAreaService, AreaService>();

            //配置认证信息
            services.AddAuthentication((options) =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters();
                options.RequireHttpsMetadata = false;
                options.Audience = Config.ApiName;//api范围
                options.Authority = Config.IdentityUrl;//IdentityServer地址
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIdentityServer();

            app.UseAuthentication();

            app.UseMvc();
        }
    }

    /// <summary>
    /// Identity扩展
    /// </summary>
    public static class IdentityExtention
    {
        /// <summary>
        /// 获取登录用户的Id
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static string GetLoginUserId(this ClaimsPrincipal identity)
        {
            return identity.Claims.FirstOrDefault(x => x.Type == Config.UserId)?.Value;
        }
    }
}
