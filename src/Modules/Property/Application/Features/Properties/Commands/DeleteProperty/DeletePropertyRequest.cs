namespace Mcm.Property.Application.Features.Properties.Commands.DeleteProperty
{
    public record DeletePropertyRequest
    (
        bool Force = false
    );
}