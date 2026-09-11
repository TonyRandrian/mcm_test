using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Company.Application.Features.Company.Queries.GetCompanyByToken
{
    public class GetCompanyByTokenResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Acronym { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Resource? Logo { get; set; } = new();
        public DateTime CreatedAt { get; set; } = new();
        public ParentCompanyToken? Parent { get; set; } = null!;
        public TypeContactDTo? TypeContact { get; set; }
        public IEnumerable<(Guid ActivitySectorId, string ActivitySectorName)>? ActivitySectors { get; set; }= [];
        public List<CategoryByTokenResponse> SupplementaryValues { get; set; }= [];
        
    }

    public class CategoryByTokenResponse
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public IEnumerable<CompanyValueToken> Informations { get; set; } = null!;
    }

    public class ParentCompanyToken
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class CompanyValueToken
    {
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; } = string.Empty;
        public object Value { get; set; } = null!;
    }

    public record TypeContactDTo
    (
        Guid Id,
        string Name,
        string? Description,
        Guid? TypeConvertTo,
        string TypeConvertToName
    );
}