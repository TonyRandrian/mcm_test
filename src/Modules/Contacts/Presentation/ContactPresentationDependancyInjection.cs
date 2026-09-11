using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Contacts.Presentation;

public static class ContactPresentationDependencyInjection
{
    public static IServiceCollection AddContactPresentation(
        this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(ContactPresentationDependencyInjection).Assembly);
        return services;
    }
}
