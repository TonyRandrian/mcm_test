namespace Mcm.Catalog.Application.Features.ServiceCategories.Queries.GetAllServiceCategory
{
    public record GetAllServiceCategoryRequest
    (
        string? searchName = null, 
        int Page = 1, 
        int Limit = 5
    );
}