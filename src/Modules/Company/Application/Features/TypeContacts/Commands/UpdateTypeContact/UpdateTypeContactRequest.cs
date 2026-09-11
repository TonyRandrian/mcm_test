using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Mcm.Company.Application.Features.TypeContacts.Commands.UpdateTypeContact
{
    public record UpdateTypeContactRequest
    (
        string? Name,
        string? Description,
        string? Color
    );
}