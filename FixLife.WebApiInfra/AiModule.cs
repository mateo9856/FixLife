using Autofac;
using FixLife.AI.Client.Abstraction;
using FixLife.AI.Client.Implementation;

namespace FixLife.WebApiInfra
{
    public class AiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GeminiClient>()
                .As<IGeminiClient>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PlanRecommendationService>()
                .As<IPlanRecomendationService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
