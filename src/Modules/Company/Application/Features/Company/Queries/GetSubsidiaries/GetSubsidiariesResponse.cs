using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Company.Application.Features.Company.Queries.GetSubsidiaries
{
    public record GetSubsidiariesResponse(
        List<SubsidiariesResponse> Subsidiaries
    );
    public class SubsidiariesResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Acronym { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Resource? Logo { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = new();
    }
}