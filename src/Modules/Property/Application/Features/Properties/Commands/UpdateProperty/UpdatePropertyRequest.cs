namespace Mcm.Property.Application.Features.Properties.Commands.UpdateProperty
{
    public record UpdatePropertyRequest
    (
        string? Name,
        string? Description,
        string? Type
    );
}