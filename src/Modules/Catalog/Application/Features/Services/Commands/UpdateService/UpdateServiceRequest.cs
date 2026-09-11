using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Mcm.Catalog.Application.Features.Services.Commands.UpdateService
{
    public record UpdateServiceRequest
    (
        string Name,
        string Description,
        decimal MaxPrice,
        decimal MinPrice,
        string Unit,
        IFormFile? CoverPicture,
        List<string>? Images,
        List<IFormFile>? NewImages,
        Guid? CategoryId,
        Guid CurrencyId
    );
}