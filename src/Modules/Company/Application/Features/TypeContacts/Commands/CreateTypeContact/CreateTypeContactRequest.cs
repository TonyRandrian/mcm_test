using Mcm.Shared.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Mcm.Company.Application.Features.TypeContacts.Commands.CreateTypeContact
{
    public record CreateTypeContactRequest
    (
        string Name,
        string? Description,
        string? Color,
        Guid? TypeConvertTo = null
    );
}