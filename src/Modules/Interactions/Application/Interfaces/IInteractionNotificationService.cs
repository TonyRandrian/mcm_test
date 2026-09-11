using Mcm.Interactions.Domain.Entities;

namespace Mcm.Interactions.Application.Interfaces
{
    public interface IInteractionNotificationService
    {
        Task SendNotification(Interaction interaction);
        Task CheckAndSendNotification();
    }
}