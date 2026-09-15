using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Contacts.Application.Features.Contacts.Queries.GetContact
{
    public class GetContactResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? CIN { get; set; } = string.Empty;
        public string Poste { get; set; } = string.Empty;
        public CompanyContactResponse Company { get; set; } = null!;
        public Resource? Image { get; set; } = null!;
        public List<CategoryContactResponse> SupplementaryValues { get; set; } = [];
    }

    public record CompanyContactResponse(Guid Id, string Name);

    public class CategoryContactResponse
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<SupplementaryDataContact> Informations { get; set; } = [];
    }
    public class SupplementaryDataContact
    {
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; } = string.Empty;
        public bool IsSensitive { get; set; }
        public string Value { get; set; } = string.Empty;
    }

}