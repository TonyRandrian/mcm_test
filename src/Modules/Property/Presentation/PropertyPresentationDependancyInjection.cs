using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Property.Presentation;

public static class PropertyPresentationDependencyInjection
{
    public static IServiceCollection AddPropertyPresentation(
        this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(PropertyPresentationDependencyInjection).Assembly);
        return services;
    }
}
