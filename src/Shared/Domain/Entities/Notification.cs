using Mcm.Shared.Domain.Primitives;

namespace Mcm.Shared.Domain.Entities
{
    public class Notification
        : BaseEntity
    {
        public string Type { get; private set; } = string.Empty;
        public string Title { get; private set; } = string.Empty;
        public DateTime StartDate { get; private set; }
        public bool IsSent { get; private set; }
        public string Message { get; private set; } = string.Empty;
        public string GroupId { get; private set; } = string.Empty;

        private Notification(string type, string title, DateTime startDate, string message, string groupId)
        {
            Type = type;
            Title = title;
            StartDate = startDate;
            Message = message;
            IsSent = false;
            GroupId = groupId;
        }

        public static Notification Create(string type, string title, DateTime startDate, string message, string groupId)
            => new(type, title, startDate, message, groupId);

        public void MarkIsSent()
        {
            IsSent = true;
        }
    }
}