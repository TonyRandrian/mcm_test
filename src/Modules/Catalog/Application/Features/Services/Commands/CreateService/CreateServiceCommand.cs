using System.Runtime.Serialization;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Catalog.Application.Features.Services.Commands.CreateService
{
    public class CreateServiceCommand
        : IRequest<ApiResponse<CreateServiceResponse>>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public string Unit { get; set; } = string.Empty;
        public IFormFile CoverPicture { get; set; } = null!;
        public List<IFormFile>? Images { get; set; } = [];
        public Guid? CategoryId { get; set; }
        public Guid CurrencyId { get; set; }
    }
}