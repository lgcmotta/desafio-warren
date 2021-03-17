using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using DesafioWarren.Application.Policies;
using DesafioWarren.Infrastructure.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioWarren.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var logger = host.Services.GetRequiredService<ILogger>();

            try
            {
                var policy = PolicyFactory.CreateAsyncRetryPolicy(logger);

                await policy.ExecuteAsync(async () =>
                {
                    await host.Services.MigrateDbContextAsync();
                });

                await host.RunAsync();
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Host exception has occurred.");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
