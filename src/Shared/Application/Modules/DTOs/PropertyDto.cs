using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Shared.Application.Modules.DTOs
{
    public record PropertyDto
    (
        Guid Id,
        Name Name,
        bool IsSystem,
        bool IsRequired,
        bool IsMultiple,
        bool IsSensitive,
        PropertyType Type
    );
}