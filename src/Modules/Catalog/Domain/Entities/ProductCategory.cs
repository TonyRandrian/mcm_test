using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;

namespace Mcm.Catalog.Domain.Entities;

public class ProductCategory : AuditableEntity, ITenantScoped
{
    public string Name { get; private set; } = string.Empty;
    public Guid TenantId { get; set; }
    public Guid? ParentCategoryId { get; private set; }
    public ProductCategory? ParentCategory { get; private set; }
    private List<ProductCategoryRelation> _productRelations = new();
    public IReadOnlyList<ProductCategoryRelation> ProductRelations => _productRelations;

    private ProductCategory() { }
    private ProductCategory(Guid? parentId, string name)
    {
        ParentCategoryId = parentId;
        Name = name;
    }

    public static ProductCategory Create(Guid? parentId, string name)
        => new(parentId, name);

    public void Update(string name)
    {
        Name = name;
        SetUpdatedAt();
    }
}
