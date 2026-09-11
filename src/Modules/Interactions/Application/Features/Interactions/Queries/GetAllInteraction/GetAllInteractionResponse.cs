using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Interactions.Application.Features.Interactions.Queries.GetAllInteraction
{
    public class GetAllInteractionResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public GetAll_Type Type { get; set; } = null!;
        public GetAll_Date Date { get; set; } = null!;
        public Guid? ReportId { get; set; }
        public int AttachmentCount { get; set; }
    }

    public record GetAll_Type(Guid Id, string Title, string LabelColor);
    public  record GetAll_Date(string startDate, string startHour, string endDate, string endHour);
}