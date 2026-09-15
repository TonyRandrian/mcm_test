using System.Text.Json;
using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace Mcm.Interactions.Application.Features.Notifications
{
    public class InteractionNotificationService(
        IConnectionManager connectionManager,
        IInteractionRepository interactionRepository,
        INotificationService notificationService,
        IHubContext<InteractionHub> hubContext)
        : IInteractionNotificationService
    {
        private readonly IHubContext<InteractionHub> _hubContext = hubContext;
        private readonly IConnectionManager _connectionManager = connectionManager;
        private readonly IInteractionRepository _interactionRepository = interactionRepository;
        private readonly INotificationService _notificationRepository = notificationService;

        public async Task CheckAndSendNotification()
        {
            var now = DateTime.UtcNow;
            var notifications = await _notificationRepository.GetNotificationsAsync();
            foreach (var notification in notifications)
            {
                if (Math.Abs((notification.StartDate - now).TotalMinutes) <= 1)
                {
                    await SendNotification(notification);
                }
            }
        }

        public async Task SendNotification(Notification notification)
        {
            var interactionId = Guid.Parse(notification.GroupId);
            var memberIds = await _interactionRepository.GetMemberIdsAsync(interactionId);
            var connectionIds = memberIds
                .SelectMany(id => _connectionManager.GetConnections(id.ToString()))
                .Distinct()
                .ToList();

            if (connectionIds.Count == 0)
                return;

            await _hubContext.Clients
                .Clients(connectionIds)
                .SendAsync("ReceiveInteractionNotification", notification);
            await _notificationRepository.MarkAsSent(notification);
        }
    }
}