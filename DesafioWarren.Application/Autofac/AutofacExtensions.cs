using Autofac;

namespace DesafioWarren.Application.Autofac
{
    public static class AutofacExtensions
    {
        public static void AddAutofacModules(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new DesafioWarrenModule());

            containerBuilder.RegisterModule(new MediatorModule("DesafioWarren.Application"
                , "DesafioWarren.Infrastructure"
                , "DesafioWarren.Domain"));
        }
    }
}