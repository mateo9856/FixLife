using Autofac;
using FixLife.WebApiInfra.Abstraction;
using FixLife.WebApiInfra.Abstraction.Dashboard;
using FixLife.WebApiInfra.Abstraction.Identity;
using FixLife.WebApiInfra.Common;
using FixLife.WebApiInfra.Contexts;
using FixLife.WebApiInfra.Services;
using FixLife.WebApiInfra.Services.Dashboard;
using FixLife.WebApiInfra.Services.Identity;

namespace FixLife.WebApiInfra
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ClientIdentityService>()
                .As<IClientIdentityService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PlanService>()
                .As<IPlanService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DashboardService>()
                .As<IDashboardService>()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(MongoContextFactory<>))
                .As(typeof(IMongoContextFactory<>))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes()
                .Where(d => d.Name.EndsWith("Service") || d.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            base.Load(builder);
        }
    }
}
