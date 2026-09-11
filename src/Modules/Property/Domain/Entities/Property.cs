using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.Exceptions;
using Mcm.Shared.Domain.Extensions;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Property.Domain.Entities
{
    public class Property : AuditableEntity, ITenantScoped
    {
        public Name Name { get; private set; } = null!;
        public string? Description
        {
            get;
            private set => field = value?.ToCapitalize(); 
        } = string.Empty;
        public bool IsSystem { get; init; }
        public bool IsRequired { get; private set; }
        public bool IsMultiple { get; private set; }
        public Guid CategoryId { get; private set; }
        public Guid TenantId { get; set;}
        public PropertyType Type { get; private set; }

        public Category Category { get; private set; } = null!;

        private Property() { }

        private Property(Guid categorieId, string name, string? description, string type, bool isRequired, bool isMultiple,  bool isSystem)
        {
            CategoryId = categorieId;
            Name = name;
            Description = description;
            Type = Enum.Parse<PropertyType>(type, ignoreCase: true);
            IsSystem = isSystem;
            IsRequired = isRequired;
            IsMultiple = isMultiple;
        }

        public static Property Create(Guid categoryId, string name, string? description = null, string type = "Text", bool isRequired = false, bool isMultiple = false, bool isSystem = false)
            => new(categoryId, name, description, type, isRequired, isMultiple, isSystem);

        public void Update(string? name, string? description, string? type)
        {
            if (name is not null) ChangeName(name);
            if (description is not null) ChangeDetail(description);
            if (type is not null) ChangeType(type);
            SetUpdatedAt();
        }

        private void ChangeName(string name) => Name = name.ToTitleCase();
        private void ChangeDetail(string description) => Description = description.ToCapitalize();
        private void ChangeType(string type)
        {
            if (Enum.TryParse<PropertyType>(type, out PropertyType parsedType))
                Type = parsedType;
        }
    }
}