namespace Mcm.Catalog.Application.Features.Products.Queries.GetAllProduct
{
    public record GetAllProductRequest
    (
        string? searchName = null, 
        int Page = 1, 
        int Limit = 5
    );
}