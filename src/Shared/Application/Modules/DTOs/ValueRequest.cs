using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Shared.Application.Modules.DTOs
{
    public record ValueRequest
    (
        Guid PropertyId,
        string Value
    );
}