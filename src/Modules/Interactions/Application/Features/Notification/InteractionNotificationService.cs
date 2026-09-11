using System.Text.Json;
using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace Mcm.Interactions.Application.Features.Notification
{
    public class InteractionNotificationService(
        IInteractionRepository interactionRepository,
        IInteractionUow uow,
        IHubContext<InteractionHub> hubContext)
        : IInteractionNotificationService
    {
        private readonly IHubContext<InteractionHub> _hubContext = hubContext;
        private readonly IInteractionUow _uow = uow;
        private readonly IInteractionRepository _interactionRepository = interactionRepository;

        public async Task CheckAndSendNotification()
        {
            var now = DateTime.UtcNow;
            var interactions = await _interactionRepository.GetAllAsync(
                predicate: i =>
                    i.Date.StartDate > now &&
                    i.Date.StartDate <= now.AddHours(2));

            foreach (var interaction in interactions)
            {
                var reminderTimes = interaction.Reminder.GetReminderTimes(interaction.Date.StartDate);
                // JsonSerializer.Serialize(reminderTimes);
                foreach (var time in reminderTimes)
                {
                    if (Math.Abs((time - now).TotalMinutes) <= 1)
                    {
                        // JsonSerializer.Serialize(interaction);
                        await SendNotification(interaction);
                    }
                }
            }
        }

        public async Task SendNotification(Interaction interaction)
        {
            var notification = Shared.Application.Common.Notification.Create(
                "Rappel",
                "Interaction",
                interaction.Date.StartDate.ToString("HH-mm"),
                interaction.Title
            );
            interaction.Reminder.MarkAsSent();

            await _hubContext.Clients.Group($"interaction:{interaction.Id}").SendAsync("ReceiveInteractionNotification", notification);
            await _uow.SaveChangesAsync();
        }
    }
}