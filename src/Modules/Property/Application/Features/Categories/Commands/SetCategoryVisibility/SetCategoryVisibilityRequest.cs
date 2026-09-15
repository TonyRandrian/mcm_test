namespace Mcm.Property.Application.Features.Categories.Commands.SetCategoryVisibility
{
    public record SetCategoryVisibilityRequest
    (
        string EntityType,
        bool IsVisibleInProfile
    );
}