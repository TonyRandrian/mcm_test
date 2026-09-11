namespace Mcm.Catalog.Application.Features.ProductCategories.Commands.DeleteProductCategory
{
    public record DeleteProductCategoryRequest
    (
        bool Force = false
    );
}