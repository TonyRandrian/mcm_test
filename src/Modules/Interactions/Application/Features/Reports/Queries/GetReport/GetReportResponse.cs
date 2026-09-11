using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Interactions.Application.Features.Reports.Queries.GetReport
{
    public class GetReportResponse
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ActionPlan { get; set; } = string.Empty;
        public GetReport_Date Date { get; set; } = null!;
        public List<GetReport_Attachment> Attachments { get; set; } = new();
        public GetReport_Contacts Contacts { get; set; } = null!;
        public GetReport_Members Members { get; set; } = null!;
    }

    public record GetReport_Date(string StartDate, string StartHour, string EndDate, string EndHour);
    public record GetReport_Attachment(string? ContentType, string Url);
    public record GetReport_Identity(Guid Id, string LastName, string FirstName);
    public record GetReport_Contacts(List<GetReport_Identity> Present, List<GetReport_Identity> Absent);
    public record GetReport_Members(List<GetReport_Identity> Present, List<GetReport_Identity> Absent);
}