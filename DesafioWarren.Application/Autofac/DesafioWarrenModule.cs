using Autofac;
using DesafioWarren.Application.Services.Identity;
using DesafioWarren.Domain.Repositories;
using DesafioWarren.Infrastructure.Dapper.Factories;
using DesafioWarren.Infrastructure.Dapper.Queries;
using DesafioWarren.Infrastructure.EntityFramework.Repositories;
using Microsoft.Extensions.Configuration;

namespace DesafioWarren.Application.Autofac
{
    public class DesafioWarrenModule : Module
    {
        private readonly IConfiguration _configuration;

        public DesafioWarrenModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountsRepository>()
                .As<IAccountRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MySqlConnectionFactory>()
                .As<IMySqlConnectionFactory>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AccountQueries>()
                .As<IAccountQueries>()
                .InstancePerLifetimeScope();

            builder.RegisterType<IdentityService>()
                .As<IIdentityService>()
                .InstancePerLifetimeScope();

        }
    }
}