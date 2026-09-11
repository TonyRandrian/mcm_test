using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Shared.Application.Modules.DTOs
{
    public record CategoryDto
    (
        Guid Id,
        Name Name,
        IEnumerable<PropertyDto> Properties
    );
}