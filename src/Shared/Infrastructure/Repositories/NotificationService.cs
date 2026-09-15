using System.Security.Cryptography.X509Certificates;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Entities;
using Mcm.Shared.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Shared.Infrastructure.Repositories
{
    public class NotificationService(SharedDbContext context)
        : INotificationService
    {
        private readonly SharedDbContext _context = context;

        public async Task CreateManyNotification(List<Notification> notifications)
        {
            await _context.Notifications.AddRangeAsync(notifications);
            await _context.SaveChangesAsync();
        }

        public async Task CreateNotification(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Notification>> GetNotificationsAsync()
        {
            DateTime now = DateTime.UtcNow;
            return await _context.Notifications
                .Where(n => n.StartDate > now && n.StartDate < now.AddMinutes(5) && !n.IsSent)
                .ToListAsync();
        }

        public async Task MarkAsSent(Notification notification)
        {
            notification.MarkIsSent();
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();
        }
    }
}