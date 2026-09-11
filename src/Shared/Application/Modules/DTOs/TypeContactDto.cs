using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Shared.Application.Modules.DTOs
{
    public record TypeContactDto
    (
        Guid Id,
        Name Name,
        string? Color
    );
}