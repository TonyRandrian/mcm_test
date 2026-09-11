using System.Text.Json.Serialization;

namespace Mcm.Property.Application.Features.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryRequest
    (
        string? Name,
        string? Description,
        List<string> EntityTypes
    );
}