using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Company.Presentation;

public static class CompanyPresentationDependencyInjection
{
    public static IServiceCollection AddCompanyPresentation(
        this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(CompanyPresentationDependencyInjection).Assembly);
        return services;
    }
}
