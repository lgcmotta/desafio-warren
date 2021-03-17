using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DesafioWarren.Application.Serilog
{
    public static class SerilogExtensions
    {
        public static IServiceCollection ConfigureSerilog(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(configuration)
                .CreateLogger();

            return serviceCollection;
        }
    }
}