using System.Reflection;
using Mcm.Authorizations.Application.Features.History.Hubs;
using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Application.Services;
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

            services.AddSignalR();
            services.AddScoped<IHashPasswordService, HashPasswordService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IActivityLogService, ActivityLogService>();

            return services;
        }
    }
}