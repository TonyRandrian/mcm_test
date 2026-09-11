namespace Mcm.Property.Application.Features.Categories.Queries.GetAllCategory
{
    public record GetAllCategoriesRequest
    (
        string? Filter = null,
        bool SystemOnly = false, 
        int Page = 1, 
        int Limit = 5
    );
}