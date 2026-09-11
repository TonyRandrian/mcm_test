using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Catalog.Application.Features.Services.Commands.UpdateService
{
    public class UpdateServiceCommand
        : IRequest<ApiResponse<UpdateServiceResponse>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public string Unit { get; set; } = string.Empty;
        public IFormFile? CoverPicture { get; set; } = null!;
        public List<string>? Images { get; set; } = [];
        public List<IFormFile>? NewImages { get; set; } = [];
        public Guid? CategoryId { get; set; }
        public Guid CurrencyId { get; set; }
    }
}