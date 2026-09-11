using System.Reflection;
using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Application.Services;
using Mcm.Company.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Authorizations.Application.Extensions
{
    public static class AuthorizationApplicationServiceExtension
    {
        public static IServiceCollection AddAuthorizationApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(assembly)
            );

            services.AddScoped<IHashPasswordService, HashPasswordService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            return services;
        }
    }
}