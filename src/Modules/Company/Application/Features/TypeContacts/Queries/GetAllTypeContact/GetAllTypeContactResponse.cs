using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Company.Application.Features.TypeContacts.Queries.GetAllTypeContact
{
    public record GetAllTypeContactResponse
    (
        List<TypeContactResponse> TypeContacts
    );


    public class TypeContactResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Color { get; set; }
    }
    
}