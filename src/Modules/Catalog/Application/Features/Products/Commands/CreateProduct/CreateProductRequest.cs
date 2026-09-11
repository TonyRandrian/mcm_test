using Mcm.Shared.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Mcm.Catalog.Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductRequest
    (
        string Name,
        string Description,
        decimal Price,
        string Unit,
        Guid CurrencyId,
        IFormFile CoverPicture,
        List<IFormFile>? Images,
        List<Guid>? CategoryIds
    );
}