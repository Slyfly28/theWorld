using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Newtonsoft.Json.Serialization;
using theWorld.Models;
using theWorld.Services;
using theWorld.ViewModels;
using TheWorld.Models;
using TheWorld.Services;
using IConfiguration = Microsoft.Framework.Configuration.IConfiguration;

namespace theWorld
{
    public class Startup
    {
        public static IConfiguration Configuration;

        public Startup(IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); // unify the JSON format of script interoperability
                }); // Add Mvc to the project

            services.AddLogging(); //Enable Logging
            services.AddEntityFramework() // register EF
                .AddSqlServer()
                .AddDbContext<WorldContext>();

            services.AddScoped<CoordService>();
            services.AddTransient<WorldContextSeedData>();
            services.AddScoped<ITheWorldRepository, TheWorldRepository>(); // Enable only one instance

        #if DEBUG
            services.AddScoped<IMailService, DebugMailService>(); // supply the services
        #else
            services.AddScoped<IMailService, RealMailService>(); 
        #endif
        }

        public void Configure(IApplicationBuilder app, WorldContextSeedData seeder, ILoggerFactory loggerfactory)
        {
            loggerfactory.AddDebug(LogLevel.Information);

            app.UseStaticFiles();

            Mapper.Initialize(config =>
            {
                config.CreateMap<Trip, TripViewModel>().ReverseMap();
                config.CreateMap<Stop, StopViewModel>().ReverseMap();
            }); //specify the different config for each types. Optimizes it.

            //start Mvc with the specified map route
            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new {controller = "App", action = "Index"}
                    );
            });

            seeder.EnsureSeedData();
        }
    }
}
