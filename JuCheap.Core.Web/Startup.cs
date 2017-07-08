using System.Threading.Tasks;
using JuCheap.Core.Data;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Services.AppServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using JuCheap.Core.Services;
using JuCheap.Core.Web.Configuration;
using JuCheap.Core.Web.Extensions;
using IdentityServer4.Validation;

namespace JuCheap.Core.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //使用Sql Server数据库
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<JuCheapContext>((serviceProvider, options) =>
                    options.UseSqlServer(Configuration.GetConnectionString("Connection_SqlServer"),
                        b => b.MigrationsAssembly("JuCheap.Core.Web"))
                        .UseInternalServiceProvider(serviceProvider));

            ////使用Sqlite数据库
            //services.AddEntityFrameworkSqlite()
            //    .AddDbContext<JuCheapContext>((serviceProvider, options) =>
            //        options.UseSqlite(Configuration.GetConnectionString("Connection_Sqlite"),
            //            b => b.MigrationsAssembly("JuCheap.Core.Web"))
            //            .UseInternalServiceProvider(serviceProvider));

            ////使用MySql数据库
            //services.AddEntityFrameworkMySql()
            //    .AddDbContext<JuCheapContext>((serviceProvider, options) =>
            //        options.UseMySql(Configuration.GetConnectionString("Connection_MySql"),
            //            b => b.MigrationsAssembly("JuCheap.Core.Web"))
            //            .UseInternalServiceProvider(serviceProvider));

            services.AddSingleton<DbContext, JuCheapContext>();

            // Add application services.
            // 1.automapper
            services.AddScoped<AutoMapper.IConfigurationProvider>(_ => AutoMapperConfig.GetMapperConfiguration());
            services.AddScoped(_ => AutoMapperConfig.GetMapperConfiguration().CreateMapper());

            // 2.service
            services.AddScoped<IDatabaseInitService, DatabaseInitService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAppService, AppService>();

            services.AddIdentityServer(options =>
            {
                options.Authentication.FederatedSignOutPaths.Add("/signout-callback-aad");
                options.Authentication.FederatedSignOutPaths.Add("/signout-callback-idsrv");
                options.Authentication.FederatedSignOutPaths.Add("/signout-callback-adfs");

                options.Events.RaiseSuccessEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseErrorEvents = true;
            })
                .AddInMemoryClients(Clients.Get())
                .AddInMemoryIdentityResources(Resources.GetIdentityResources())
                .AddDeveloperSigningCredential()
                .AddExtensionGrantValidator<Extensions.ExtensionGrantValidator>()
                .AddExtensionGrantValidator<NoSubjectExtensionGrantValidator>()
                .AddSecretParser<ClientAssertionSecretParser>()
                .AddSecretValidator<PrivateKeyJwtSecretValidator>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentityServer();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //init database
            var dbService = app.ApplicationServices.GetRequiredService<IDatabaseInitService>();
            Task.Run(() => dbService.InitAsync());
        }
    }
}
