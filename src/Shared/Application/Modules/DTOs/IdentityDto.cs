using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Shared.Application.Modules.DTOs
{
    public record IdentityDto
    (
        Guid Id, 
        string LastName, 
        string FirstName, 
        string Image,
        string Email
    );
}