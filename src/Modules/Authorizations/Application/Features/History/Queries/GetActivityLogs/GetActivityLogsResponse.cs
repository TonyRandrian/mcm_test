namespace Mcm.Authorizations.Application.Features.History.Queries.GetActivityLogs
{
    public class GetActivityLogsResponse
    {
        public List<ActivityLogResponse> Logs { get; set; } = [];
    }

    public class ActivityLogResponse
    {
        public Guid Id { get; set; }       
        public string EventType { get; set; } = string.Empty;       
        public string Category { get; set; } = string.Empty;       
        public string Label { get; set; } = string.Empty;       
        public string Detail { get; set; } = string.Empty;       
        public ActivityLogs_Author Author { get; set; } = new();
        public DateTime OccuredAt { get; set; }       
    }

    public class ActivityLogs_Author
    {
        public Guid? Id { get; set; } = Guid.Empty;
        public string FullName { get; set; } = "Système";
    }
}