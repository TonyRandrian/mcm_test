using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Mcm.Catalog.Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductRequest
    (
        string Name,
        string Description,
        decimal Price,
        string Unit,
        IFormFile? CoverPicture,
        List<string>? Images,
        List<IFormFile>? NewImages,
        List<Guid>? CategoryIds,
        Guid CurrencyId
    );
}