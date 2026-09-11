using Mcm.Shared.Application.Common;

namespace Mcm.Catalog.Application.Features.Products.Queries.GetProduct
{
    public class GetProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Unit { get; set; } = string.Empty;
        public ResourceResponse CoverPicture { get; set; } = null!;
        public ProductCurrency Currency { get; set; } = null!;
        public List<ResourceResponse>? Images { get; set; }
        public List<ProductCategoryResponse>? Categories { get; set; }
    }

    public class ProductCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class ProductCurrency
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
    }
}  