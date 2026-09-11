using Mcm.Shared.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Mcm.Contacts.Application.Features.Contacts.Commands.UpdateContact
{
    public record UpdateContactRequest
    (
        string LastName,
        string FirstName,
        string? Poste,
        string? Email,
        string? Cin,
        string? Phone,
        IFormFile? Image,
        Guid AssociatedCompanyId
    );
}