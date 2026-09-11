using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Company.Domain.Entities
{
    public class TypeContact : AuditableEntity, ITenantScoped
    {
        public Name Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public string? Color { get; private set; }
        public Guid TenantId { get; set; }
        public Guid? TypeConvertTo { get; set; }
        public TypeContact? TypeConvertToNavigation { get; private set; }

        private TypeContact() { }
        private TypeContact(string name, string? description, string? color, Guid? typeConvertTo)
        {
            Name = name;
            Description = description;
            Color = color;
            TypeConvertTo = typeConvertTo;
        }
        
        public static TypeContact Create(string name, string? description, string? color = null, Guid? typeConvertTo = null)
            => new(name, description, color, typeConvertTo);

        public void Update(string? name, string? description, string? color)
        {
            if (name is not null) ChangeName(name);
            if (description is not null && color is not null) ChangeDetail(description, color);
            SetUpdatedAt();
        }

        private void ChangeName(string name) => Name = name;
        private void ChangeDetail(string description, string color) => (Description, Color) = (description, color);
    }
}