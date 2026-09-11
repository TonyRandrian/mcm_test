using System.Runtime.Serialization;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Catalog.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ApiResponse<CreateProductResponse>>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Unit { get; set; } = string.Empty;
        public Guid CurrencyId { get; set; }
        public IFormFile CoverPicture { get; set; } = null!;
        public List<IFormFile>? Images { get; set; }
        public List<Guid>? CategoryIds { get; set; } = [];
    }
}