using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Contacts.Application.Extensions
{
    public static class AuthorizationApplicationServiceExtension
    {
        public static IServiceCollection AddContactApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(assembly)
            );

            return services;
        }
    }
}