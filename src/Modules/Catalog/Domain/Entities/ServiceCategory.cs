using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;

namespace Mcm.Catalog.Domain.Entities;

public class ServiceCategory : AuditableEntity, ITenantScoped
{
    public string Name { get; set; } = string.Empty;
    public Guid TenantId { get; set; }
    public Guid? ParentCategoryId { get; private set; }
    public ServiceCategory? ParentCategory { get; private set; }
    private List<Service> _services = new();
    public IReadOnlyList<Service> Services => _services;

    private ServiceCategory() { }
    private ServiceCategory(Guid? parentId, string name)
    {
        ParentCategoryId = parentId;
        Name = name;
    }

    public static ServiceCategory Create(Guid? parentId, string name)
        => new(parentId, name);

    public void Update(string name)
    {
        Name = name;
        SetUpdatedAt();
    }

}