namespace Mcm.Catalog.Application.Features.Services.Commands.DeleteService
{
    public record DeleteServiceRequest
    (
        bool Force = false
    );
}