using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Catalog.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand
        : IRequest<ApiResponse<UpdateProductResponse>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Unit { get; set; } = string.Empty;
        public IFormFile? CoverPicture { get; set; } = null!;
        public List<string>? Images { get; set; } = [];
        public List<IFormFile>? NewImages { get; set; } = null!;
        public List<Guid>? CategoryIds { get; set; } = [];
        public Guid CurrencyId { get; set; }
    }
}