using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using A2.Web.SportNews.Options;
using Autofac.Extensions.DependencyInjection;
using Serilog;

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

                Log.Information("Host is successfully built.");
                await host.RunAsync();
                Log.Information("Application is stopped. Exited clearly.");
            }
            catch (Exception e)
            {
                Log.Fatal("An unhandled exception occurred during bootstrapping", e);
            }
            finally
            {
                Log.CloseAndFlush();
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
                            SetLogs(configName);

                            config.SetBasePath(Directory.GetCurrentDirectory());
                            config.AddJsonFile("Config/" + configName, optional: false, reloadOnChange: false);

                            config.Build();
                        })
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseIISIntegration()
                        .UseStartup<Startup>();
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory());

        private static void SetLogs(string configName)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("Config/" + configName, optional: false)
                .Build();

            AppOptions appOptions = new AppOptions();
            config.GetSection(AppOptions.SectionName).Bind(appOptions);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(Path.Combine(appOptions.LogSaveAbsolutePath, "log-.txt"),
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
