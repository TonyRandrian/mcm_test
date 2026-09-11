using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Interactions.Application.Features.Interactions.Queries.GetInteraction
{
    public class GetInteractionResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Note { get; set; }
        public Get_Type Type { get; set; } = null!;
        public Get_Date Date { get; set; } = null!;
        public Get_Reminder Reminder { get; set; } = null!;
        public Guid? ReportId { get; set; }
        public List<Get_Attachment> Attachments { get; set; } = new();
        public Get_Participant Participant { get; set; } = null!;
        public List<Get_Information> Informations { get; set; } = new();
    }
    
    public record Get_Type(Guid Id, string Title, string LabelColor);
    public  record Get_Date(string startDate, string startHour, string endDate, string endHour);
    public  record Get_Reminder(string Type, double Value, int Repeat);
    public  record Get_Attachment(string? ContentType, string Url);
    public  record Get_Identity(Guid Id, string FirstName, string LastName, string Image, string Email);
    public  record Get_Information(Guid FieldId, string FieldName, string Value);
    public  record Get_Participant(List<Get_Identity> Members, List<Get_Identity> Contacts);
}