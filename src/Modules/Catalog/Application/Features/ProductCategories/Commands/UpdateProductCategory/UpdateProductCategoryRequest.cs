using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Mcm.Catalog.Application.Features.ProductCategories.Commands.UpdateProductCategory
{
    public record UpdateProductCategoryRequest
    (
        string Name
    );
}