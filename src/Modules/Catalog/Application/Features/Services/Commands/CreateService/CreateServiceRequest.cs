using Mcm.Shared.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Mcm.Catalog.Application.Features.Services.Commands.CreateService
{
    public record CreateServiceRequest
    (
        string Name,
        string Description,
        decimal? MaxPrice,
        decimal? MinPrice,
        string Unit,
        Guid CurrencyId,
        IFormFile CoverPicture,
        List<IFormFile>? Images,
        Guid? CategoryId
    );
}