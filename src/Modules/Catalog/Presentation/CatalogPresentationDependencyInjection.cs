using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Catalog.Presentation;

public static class CatalogPresentationDependencyInjection
{
    public static IServiceCollection AddCatalogPresentation(
        this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(CatalogPresentationDependencyInjection).Assembly);
        return services;
    }
}
