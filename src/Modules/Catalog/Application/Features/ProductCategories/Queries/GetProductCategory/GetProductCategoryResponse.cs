using Mcm.Shared.Application.Common;

namespace Mcm.Catalog.Application.Features.ProductCategories.Queries.GetProductCategory
{
    public class GetProductCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Count { get; set; }
    }
}  