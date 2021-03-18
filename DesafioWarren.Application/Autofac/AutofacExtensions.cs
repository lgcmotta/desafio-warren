using Autofac;
using Microsoft.Extensions.Configuration;

namespace DesafioWarren.Application.Autofac
{
    public static class AutofacExtensions
    {
        public static void AddAutofacModules(this ContainerBuilder containerBuilder, IConfiguration configuration)
        {
            containerBuilder.RegisterModule(new DesafioWarrenModule(configuration));

            containerBuilder.RegisterModule(new MediatorModule("DesafioWarren.Application"
                , "DesafioWarren.Infrastructure"
                , "DesafioWarren.Domain"));
        }
    }
}