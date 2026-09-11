using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Catalog.Application.Features.ProductCategories.Queries.GetAllProductCategory
{
    public record GetAllProductCategoryResponse
    (
        List<ProductCategories> Categories
    );

    public class ProductCategories
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Count { get; set; }
        public List<ProductCategories> SubCategories { get; set; } = [];
    }
    
}