using System.Reflection;
using Mcm.Interactions.Application.Features.Notification;
using Mcm.Interactions.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Interactions.Application.Extensions
{
    public static class InteractionApplicationServiceExtension
    {
        public static IServiceCollection AddInteractionApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(assembly)
            );

            services.AddSignalR(options =>
                options.EnableDetailedErrors = true);
            services.AddScoped<IInteractionNotificationService, InteractionNotificationService>();
            services.AddHostedService<InteractionService>();

            return services;
        }
    }
}