namespace Mcm.Catalog.Application.Features.ServiceCategories.Commands.DeleteServiceCategory
{
    public record DeleteServiceCategoryRequest
    (
        bool Force = false
    );
}