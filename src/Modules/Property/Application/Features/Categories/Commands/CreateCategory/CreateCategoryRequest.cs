namespace Mcm.Property.Application.Features.Categories.Commands.CreateCategory
{
    public record CreateCategoryRequest
    (
        string Name,
        string? Description,
        string[] EntityTypes
    );
}