using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Catalog.Application.Features.Products.Queries.GetAllProduct
{
    public record GetAllProductResponse
    (
        List<ProductResponse> Products
    );


    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Decimal Price { get; set; }
        public string Unit { get; set; } = string.Empty;
        public ResourceResponse CoverPicture { get; set; } = null!;
        public ProductCurrencyResponse Currency { get; set; } = null!;
        public List<ResourceResponse>? Images { get; set; } = [];
        public List<ProductCategoriesResponse>? Categories { get; set; } = [];
    
    }

    public class ProductCategoriesResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class ProductCurrencyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
    }
    
}