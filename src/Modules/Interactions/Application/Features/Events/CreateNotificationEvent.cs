using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Interactions.Domain.Events;
using Mcm.Interactions.Domain.ValueObjects;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Entities;
using MediatR;

namespace Mcm.Interactions.Application.Features.Events
{
    public class CreateNotificationEvent(
        INotificationService notificationService)
        : INotificationHandler<InteractionCreatedEvent>
    {
        private readonly INotificationService _notificationService = notificationService;
        public async Task Handle(InteractionCreatedEvent notification, CancellationToken cancellationToken)
        {
            List<Notification> notifications = [];
            foreach (var date in notification.Reminder.GetReminderTimes(notification.Date.StartDate))
            {
                notifications.Add(Notification.Create(
                    "Rappel",
                    "Interaction",
                    date,
                    "Une activité va commencer prochainement",
                    notification.InteractionId.ToString()
                ));
            }
            notifications.Add(Notification.Create(
                "Rappel",
                "Compte-Rendu",
                notification.Date.EndDate.AddHours(1),
                "Veuillez completer le Compte Rendu du recent activité",
                notification.InteractionId.ToString()
            ));

            await _notificationService.CreateManyNotification(notifications);
        }
    }
}