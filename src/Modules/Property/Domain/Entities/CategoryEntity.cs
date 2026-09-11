using Mcm.Property.Domain.Enums;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;

namespace Mcm.Property.Domain.Entities
{
    public class CategoryEntity : AggregateRoot, ITenantScoped
    {
        public Guid CategoryId { get; private set; }
        public Guid TenantId { get; set; }
        public Category Category { get; private set; } = null!;
        public EntityType EntityType { get; private set; }

        private CategoryEntity() { }
        private CategoryEntity(string entity, Guid categoryId, Guid tenantId)
        {
            EntityType = Enum.Parse<EntityType>(entity);
            CategoryId = categoryId;
            TenantId = tenantId;
        }

        public static CategoryEntity Create(string entityName, Guid categoryId, Guid tenantId)
            => new(entityName, categoryId, tenantId);
    }
}