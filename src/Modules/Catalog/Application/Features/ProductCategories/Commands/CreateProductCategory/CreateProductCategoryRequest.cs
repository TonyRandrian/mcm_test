using Mcm.Shared.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Mcm.Catalog.Application.Features.ProductCategories.Commands.CreateProductCategory
{
    public record CreateProductCategoryRequest
    (
        Guid? ParentCategoryId,
        string Name
    );
}