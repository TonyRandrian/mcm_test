using Mcm.Property.Domain.Enums;
using Mcm.Shared.Domain.Exceptions;
using Mcm.Shared.Domain.Extensions;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Property.Domain.Entities
{
    public class Category : AggregateRoot, ITenantScoped
    {
        public Name Name { get; private set; } = null!;
        public string? Description 
        {
            get;
            private set => field = value?.ToCapitalize(); 
        } = string.Empty;
        public bool IsSystem { get; set; }
        public Guid TenantId { get; set; }

        private readonly List<CategoryEntity> _entities = [];
        private readonly List<Property> _properties = [];
        public IReadOnlyList<CategoryEntity> Entities => _entities.AsReadOnly();
        public IReadOnlyList<Property> Properties => _properties.AsReadOnly();

        private Category() {}

        private Category(string name, string? description, bool isSystem)
        {
            Name = name;
            Description = description;
            IsSystem = isSystem;
        }

        public static Category Create(string name, string? description = null, bool isSystem = false)
             => new(name, description, isSystem);

        public void Update(string? name, string? description)
        {
            if (name is not null)
                ChangeName(name);
            if (description is not null)
                ChangeDetail(description);
            SetUpdatedAt();
        }

        public void AddProperty(Property property)
        {
            if (_properties.Any(p => p.Name.Equals(property.Name)))
                return;
            _properties.Add(property);
        }

        public void RemoveProperty(Property property)
        {
            var exist = _properties.FirstOrDefault(p => p.Name.Equals(property.Name));
            if (exist is not null)
                _properties.Remove(property);
        }

        public void AddEntityType(string entityName)
        {
            var catEnt = CategoryEntity.Create(entityName, Id, TenantId);
            if (_entities.Any(e => e.EntityType.Equals(catEnt.EntityType)))
                return;
            _entities.Add(catEnt);
        }

        public void AddGroupEntityType(List<string> entitiesName)
        {
            foreach (var entity in entitiesName)
            {
                this.AddEntityType(entity);
            }
        }

        public void SyncListEntityType(List<string>? entitiesName)
        {
            if (entitiesName is null)
                return;
            var hashEntities = entitiesName.ToHashSet();
            _entities.RemoveAll(e => 
                !hashEntities.Contains(e.EntityType.ToString()));
            foreach (var entityName in hashEntities)
            {
                if (!_entities.Any(e => e.EntityType.ToString() == entityName))
                {
                    var catEnt = CategoryEntity.Create(entityName, Id, TenantId);
                    _entities.Add(catEnt);
                }
            }
        }

        public void RemoveEntityType(string entityName)
        {
            if (!Enum.TryParse<EntityType>(entityName, out EntityType parsedType))
                throw new DomainException("Unkown entity type.");

            var catEnt = _entities.FirstOrDefault(x => x.EntityType == parsedType)
                ?? throw new DomainException("Not found the specified entity type.");
            _entities.Remove(catEnt);
        }

        private void ChangeName(string name)
            => Name = name.ToTitleCase();
        private void ChangeDetail(string description)
            => Description = description.ToCapitalize();
    }
}