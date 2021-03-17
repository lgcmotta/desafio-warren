using Autofac;
using DesafioWarren.Domain.Repositories;
using DesafioWarren.Infrastructure.EntityFramework.Repositories;

namespace DesafioWarren.Application.Autofac
{
    public class DesafioWarrenModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountsRepository>()
                .As<IAccountRepository>()
                .InstancePerLifetimeScope();
        }
    }
}