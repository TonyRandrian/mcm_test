using Mcm.Property.Domain.Enums;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;

namespace Mcm.Property.Domain.Entities
{
    public sealed class CategorySetting
        : AggregateRoot, ITenantScoped
    {
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; } = null!;
        public EntityType EntityType { get; private set; }
        public Guid TenantId { get; set; }
        public bool IsVisibleInProfile { get; private set; } = true;
        
        private CategorySetting() { }

        private CategorySetting(Guid categoryId, EntityType entityType, bool isVisibleInProfile)
        {
            CategoryId = categoryId;
            EntityType = entityType;
            IsVisibleInProfile = isVisibleInProfile;
        }

        public static CategorySetting Create(Guid categoryId, EntityType entityType, bool isVisibleInProfile)
            => new(categoryId, entityType, isVisibleInProfile);

        public void Update(bool isVisibleInProfile)
        {
            IsVisibleInProfile = isVisibleInProfile;
        }
    }
}