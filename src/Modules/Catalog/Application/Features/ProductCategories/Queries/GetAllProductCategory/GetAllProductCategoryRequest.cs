namespace Mcm.Catalog.Application.Features.ProductCategories.Queries.GetAllProductCategory
{
    public record GetAllProductCategoryRequest
    (
        string? searchName = null, 
        int Page = 1, 
        int Limit = 5
    );
}