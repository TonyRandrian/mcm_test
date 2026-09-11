using System.Reflection;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Catalog.Application.Extensions
{
    public static class CatalogApplicationServiceExtension
    {
        public static IServiceCollection AddCatalogApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg
                => cfg.RegisterServicesFromAssembly(assembly));

            return services;
        }
    }
}