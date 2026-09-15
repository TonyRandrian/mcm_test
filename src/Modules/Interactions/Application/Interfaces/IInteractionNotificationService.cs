using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Domain.Entities;

namespace Mcm.Interactions.Application.Interfaces
{
    public interface IInteractionNotificationService
    {
        Task CheckAndSendNotification();
        Task SendNotification(Notification notification);
    }
}