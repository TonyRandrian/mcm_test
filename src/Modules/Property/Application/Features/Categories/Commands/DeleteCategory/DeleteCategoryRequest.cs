namespace Mcm.Property.Application.Features.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryRequest
    (
        bool Force = false
    );
}