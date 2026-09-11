namespace Mcm.Shared.Application.Common
{
    public class Notification
    {
        public string Type { get; private set; } = string.Empty;
        public string Title { get; private set; } = string.Empty;
        public string StartDate { get; private set; } = string.Empty;
        public string Message { get; private set; } = string.Empty;

        private Notification(string type, string title, string startDate, string message)
        {
            Type = type;
            Title = title;
            StartDate = startDate;
            Message = message;
        }

        public static Notification Create(string type, string title, string startDate, string message)
            => new(type, title, startDate, message);
            
    }
}