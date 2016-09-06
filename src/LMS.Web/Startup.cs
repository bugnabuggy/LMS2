using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Infrastructure.Data;
using LMS.Utilities.Common;
using LMS.Web.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Microsoft.Extensions.Configuration;
using LMS.Web.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;

using LMS.Utilities.Logging;

namespace LMS.Web
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        private IHostingEnvironment _environment;
        private ILogManager _logManager;


        public Startup(IHostingEnvironment env)
        {
            _environment = env;

            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddJsonFile($"appsettings.private.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            //Logging bootstraping
            _logManager = new LMS.Utilities.Logging.LogManager();
            var section = Configuration.GetSection("Logging");
            // clear text keys from 'Logging:' prefix
            var path = section.Path + ":";
            _logManager.Configure(section.AsEnumerable().Select(x=> new KeyValuePair<string, string>(x.Key.Replace(path,""), x.Value)));

            _logManager.GetLogger().Warn("LMS.Web is starting");

            //DB check and recreate
            CheckDb<LmsDbContext>(Configuration.GetConnectionString("DefaultConnection"));
            CheckDb<AppDbContext>(Configuration.GetConnectionString("DefaultConnection"));

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LmsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<ILmsContext, LmsDbContext>();

            services.AddSingleton<ILogManager>(_logManager);

            services.AddMvc();

            services.AddOptions();

            services.Configure<Settings>(Configuration);
            services.Configure<Settings>(x => {
                x.Wwwroot = _environment.WebRootPath;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "api",
                    template: "api/{controller}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Main}/{action=Index}/{id?}");
            });
            app.UseStatusCodePages();
        }

        private IOperationResult CheckDb<T>(string connectionString) where T : DbContext
        {
            _logManager.GetLogger().Info($"Checking db context {typeof(T).Name}");
            var result = new OperationResult() { Success = true };
            var optionsBuilderApp = new DbContextOptionsBuilder<T>();
            optionsBuilderApp.UseSqlServer(connectionString);

            using (var context = Activator.CreateInstance(typeof(T), new object[1] { optionsBuilderApp.Options }) as T)
            {
                var migrator = context.GetInfrastructure().GetRequiredService<IMigrator>();
                migrator.Migrate();
            }

            return result;
        }
    }
}
