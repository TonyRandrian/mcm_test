using Mcm.Shared.Domain.Entities;

namespace Mcm.Shared.Application.Interfaces
{
    public interface INotificationService
    {
        Task CreateNotification(Notification notification);
        Task CreateManyNotification(List<Notification> notifications);
        Task MarkAsSent(Notification notification);
        Task<List<Notification>> GetNotificationsAsync();
    }
}