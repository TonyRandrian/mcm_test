using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Contacts.Application.Features.Contacts.Queries.GetAllContact
{
    public record GetAllContactResponse
    (
        List<ContactResponse> Contacts
    );

    public class ContactResponse
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; } 
        public string? Phone { get; set; }
        public string? CIN { get; set; }
        public string? Poste { get; set; }
        public CompanyContactsResponse? Company { get; set; } = null!;
        public Resource? Image { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public List<ProjectedValue>? Values { get; set; }
        public TypeContactResponse? TypeContact { get; set; } = null;
    }

    public record CompanyContactsResponse(Guid Id, string Name);
    public class ProjectedValue
    {
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
    public class TypeContactResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Color { get; set; } = string.Empty;
    }
}