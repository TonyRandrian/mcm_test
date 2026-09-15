using System.Runtime.CompilerServices;
using Mcm.Interactions.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Mcm.Interactions.Application.Features.Notifications
{
    public class InteractionService(
        ILogger<InteractionService> logger,
        IServiceProvider serviceProvider)
        : BackgroundService
    {
        private readonly ILogger<InteractionService> _logger = logger;
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var service = scope.ServiceProvider.GetRequiredService<IInteractionNotificationService>();
                        await service.CheckAndSendNotification();
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error signalR");
                }
                finally
                {
                    await Task.Delay(2000, stoppingToken);
                }
            }
        }
    }
}