using Autofac;
using EntityManagement;
using EntityManagement.Core;
using SampleApiWebApp.Data;

namespace SampleApiWebApp.Configuration
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseContext>()
                .As<IDatabaseContext>()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EntityRepository<,>))
                .As(typeof(IEntityRepository<,>));
        }
    }
}
