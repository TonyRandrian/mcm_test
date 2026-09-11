using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Interactions.Presentation;

public static class InteractionPresentationDependencyInjection
{
    public static IServiceCollection AddInteractionPresentation(
        this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(InteractionPresentationDependencyInjection).Assembly);
        return services;
    }
}
