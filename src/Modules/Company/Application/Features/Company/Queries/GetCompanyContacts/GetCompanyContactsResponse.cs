using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Company.Application.Features.Company.Queries.GetCompanyContacts
{
    public record GetCompanyContactsResponse(
        List<CompanyContactDto> CompanyContacts
    );
    public class CompanyContactDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Acronym { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Resource? Logo { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = new();
    }
}