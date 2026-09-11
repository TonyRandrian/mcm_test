using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Authorizations.Presentation;

public static class AuthorizationPresentationDependencyInjection
{
    public static IServiceCollection AddAuthorizationPresentation(
        this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(AuthorizationPresentationDependencyInjection).Assembly);
        return services;
    }
}
