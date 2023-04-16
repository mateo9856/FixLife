using Autofac;
using Autofac.Core;
using FixLife.WebApiInfra.Abstraction;
using FixLife.WebApiInfra.Abstraction.Identity;
using FixLife.WebApiInfra.Services;
using FixLife.WebApiInfra.Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiInfra
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ClientIdentityService>().As<IClientIdentityService>()
            .InstancePerLifetimeScope();

            builder.RegisterType<PlanService>().As<IPlanService>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes()
                .Where(d => d.Name.EndsWith("Service") || d.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            base.Load(builder);
        }
    }
}
