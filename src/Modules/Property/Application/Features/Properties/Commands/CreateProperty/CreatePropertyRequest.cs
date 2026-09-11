using System.Text.Json.Serialization;

namespace Mcm.Property.Application.Features.Properties.Commands.CreateProperty
{
    public record CreatePropertyRequest
    (
        string Name,
        string? Description,
        string Type,
        bool IsRequired,
        bool IsMultiple
    );
}
