using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;

namespace A2.Web.SportNews
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                IHostBuilder hostBuilder = CreateHostBuilder(args);
                var host = hostBuilder.Build();
                await host.RunAsync();
            }
            catch (Exception e)
            {
                
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    webHostBuilder
                        .ConfigureAppConfiguration((hostingContext, config) =>
                        {
                            var configName = hostingContext.HostingEnvironment.EnvironmentName + ".json";

                            config.SetBasePath(Directory.GetCurrentDirectory());
                            config.AddJsonFile("Config/" + configName, optional: false, reloadOnChange: false);

                            config.Build();
                        })
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseIISIntegration()
                        .UseStartup<Startup>();
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
