using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Catalog.Domain.Entities
{
    public class ProductCategoryRelation : ITenantScoped
    {
        public Guid ProductId { get; private set; }
        public Product Product { get; set; } = null!;

        public Guid CategoryId { get; private set; }
        public ProductCategory Category { get; set; } = null!;
        public Guid TenantId { get; set; }

        private ProductCategoryRelation() { }
        private ProductCategoryRelation(Guid productId, Guid categoryId)
        {
            ProductId = productId;
            CategoryId = categoryId;
        }

        public static ProductCategoryRelation Create(Guid productId, Guid categoryId)
        => new(productId, categoryId);

    }
}