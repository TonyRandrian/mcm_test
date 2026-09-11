using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Catalog.Application.Features.Services.Queries.GetAllService
{
    public record GetAllServiceResponse
    (
        List<ServiceResponse> Services
    );


    public class ServiceResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public string Unit { get; set; } = string.Empty;
        public ResourceResponse CoverPicture { get; set; } = null!;
        public List<ResourceResponse> Images { get; set; } = null!;
        public string Category { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
    }
    
}