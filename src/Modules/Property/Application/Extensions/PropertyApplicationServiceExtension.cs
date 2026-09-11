using System.Reflection;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Property.Application.Extensions
{
    public static class PropertyApplicationServiceExtension
    {
        public static IServiceCollection AddPropertyApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg
                => cfg.RegisterServicesFromAssembly(assembly));

            return services;
        }
    }
}