using System.Reflection;
using Mcm.Company.Application.Features.Dashboard;
using Mcm.Company.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Company.Application.Extensions
{
    public static class CompanyApplicationServiceExtension
    {
        public static IServiceCollection AddCompanyApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(assembly)
            );

            services.AddScoped<ICompanyDashboardService, CompanyDashboardService>();

            return services;
        }
    }
}