using System.Collections.Generic;
using System.Reflection;
using Autofac;
using MediatR;
using MediatR.Pipeline;

namespace RequestManagement
{
    /// <summary>
    /// Request Management Autofac Module
    /// </summary>
    public class RequestManagementModule : Autofac.Module
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestManagementModule"/> class
        /// </summary>
        /// <param name="mediatrAssemblies">Mediatr assemblies</param>
        public RequestManagementModule(IEnumerable<Assembly> mediatrAssemblies)
        {
            MediatrAssemblies = mediatrAssemblies;
        }

        private IEnumerable<Assembly> MediatrAssemblies { get; }

        /// <summary>
        /// Registers RequestManagement components to dependency injection container
        /// </summary>
        /// <param name="builder">Container builder</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
            };

            var assemblies = new List<Assembly>();
            assemblies.AddRange(MediatrAssemblies);
            assemblies.Add(typeof(OperationResult).Assembly);

            foreach (var assembly in assemblies)
            {
                foreach (var mediatrOpenType in mediatrOpenTypes)
                {
                    builder
                        .RegisterAssemblyTypes(assembly)
                        .AsClosedTypesOf(mediatrOpenType)
                        .AsImplementedInterfaces();
                }
            }

            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
