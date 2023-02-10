using FixLife.WebApiQueries.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FixLife.WebApiQueries
{
    public static class AppConfig
    {
        public static IServiceCollection AddQueriesDependecies(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(typeof(AutoMapperConfig));

            services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(LoggingBehavior<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}