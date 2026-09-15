using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Company.Application.Features.Company.Queries.GetCompanyById
{
    public class GetCompanyByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Acronym { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Resource? Logo { get; set; } = new();
        public DateTime CreatedAt { get; set; } = new();
        public TypeContactDto? TypeContact { get; set; }
        public ParentCompany? Parent { get; set; }= null!;
        public IEnumerable<CategoryByIdResponse> SupplementaryValues { get; set; }= [];
        public IEnumerable<CompanyActivityDto>? ActivitySectors { get; set; }= [];
        
    }

    public record CategoryByIdResponse
    (
        Guid CategoryId,
        string CategoryName,
        bool IsVisible,
        IEnumerable<CompanyValue> Informations
    );
    public record CompanyValue
    (
        Guid PropertyId,
        string PropertyName,
        bool IsSensitive,
        string Value
    );
    public record ParentCompany
    (
        Guid Id,
        string Name
    );

    public record CompanyActivityDto
    (
        Guid ActivitySectorId,
        string ActivitySectorName
    );

    public record TypeContactDto
    (
        Guid Id,
        string Name,
        string? Color,
        TypeContactDto? TypeConvertTo = null
    );

}