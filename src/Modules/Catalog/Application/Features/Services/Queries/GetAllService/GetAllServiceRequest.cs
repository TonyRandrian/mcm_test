namespace Mcm.Catalog.Application.Features.Services.Queries.GetAllService
{
    public record GetAllServiceRequest
    (
        string? SearchName = null, 
        int Page = 1, 
        int Limit = 5
    );
}