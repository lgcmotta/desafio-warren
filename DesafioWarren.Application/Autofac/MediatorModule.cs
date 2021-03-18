using System;
using System.Reflection;
using Autofac;
using DesafioWarren.Application.Behaviours;
using FluentValidation;
using MediatR;
using Module = Autofac.Module;

namespace DesafioWarren.Application.Autofac
{
    public class MediatorModule : Module
    {
        private readonly string[] _assembliesNames;

        public MediatorModule(params string[] assembliesNames)
        {
            _assembliesNames = assembliesNames;
        }

        protected override void Load(ContainerBuilder builder)
        {
            foreach (var assemblyName in _assembliesNames)
            {
                var assembly = AppDomain.CurrentDomain.Load(assemblyName);

                LoadModules(builder, assembly);
            }
            
            builder.Register<ServiceFactory>(context =>
            {
                var component = context.Resolve<IComponentContext>();

                return type => component.TryResolve(type, out var obj) ? obj : null;
            });

            builder.RegisterGeneric(typeof(LoggingBehaviour<,>)).As(typeof(IPipelineBehavior<,>));

            builder.RegisterGeneric(typeof(ValidationBehaviour<,>)).As(typeof(IPipelineBehavior<,>));

            builder.RegisterGeneric(typeof(TransactionalBehaviour<,>)).As(typeof(IPipelineBehavior<,>));
        }

        private static void LoadModules(ContainerBuilder builder, Assembly assembly)
        {
            AssemblyScanner.FindValidatorsInAssembly(assembly)
                .ForEach(scannedAssembly => builder.RegisterType(scannedAssembly.ValidatorType).As(scannedAssembly.InterfaceType).InstancePerLifetimeScope());
            
            builder.RegisterType<Mediator>().As<IMediator>().AsImplementedInterfaces();
            
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(INotificationHandler<>));
        }
    }
}