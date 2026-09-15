// src/Shared/Infrastructure/Extensions/DomainEventHandlerExtension.cs
using System.Reflection;
using Mcm.Authorizations.Application.Features.EventHandler;
using Mcm.Shared.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Shared.Infrastructure.Extensions
{
    public static class DomainEventHandlerExtension
    {
        public static IServiceCollection AddDomainEventHandlers(this IServiceCollection services)
        {
            services.AddScoped<ActivityLogHandler>();

            var domainEventTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName != null && a.FullName.StartsWith("Mcm"))
                .SelectMany(GetLoadableTypes)
                .Where(t => t is { IsClass: true, IsAbstract: false }
                    && typeof(IDomainEvent).IsAssignableFrom(t));

            foreach (var eventType in domainEventTypes)
            {
                var handlerInterface = typeof(INotificationHandler<>).MakeGenericType(eventType);
                // ActivityLogHandler (INotificationHandler<IDomainEvent>) est castable en
                // INotificationHandler<TConcret> grâce à la contravariance du "in TNotification"
                services.AddTransient(handlerInterface, sp => sp.GetRequiredService<ActivityLogHandler>());
            }

            return services;
        }

        private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
        {
            try { return assembly.GetTypes(); }
            catch (ReflectionTypeLoadException ex) { return ex.Types.Where(t => t is not null)!; }
        }
    }
}