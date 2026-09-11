using Mcm.Shared.Application.Common;

namespace Mcm.Catalog.Application.Features.Services.Queries.GetService
{
    public class GetServiceResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public string Unit { get; set; } = string.Empty;
        public ResourceResponse CoverPicture { get; set; } = null!;
        public List<ResourceResponse>? Images  { get; set; } = [];
        public string? Category { get; set; }
        public string Currency { get; set; } = string.Empty;
    }
}