using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Interactions.Domain.Entities
{
    public class TypeField : AuditableEntity, ITenantScoped
    {
        public Name Name { get; private set; } = null!;
        public PropertyType Type { get; private set; }
        public Guid InteractionTypeId { get; private set; }
        public Guid TenantId { get; set; }
        
        public InteractionType InteractionType { get; private set; } = null!;

        private TypeField() {}
        private TypeField(Guid interactionType, string name, string type)
        {
            InteractionTypeId = interactionType;
            Name = name;
            if (Enum.TryParse<PropertyType>(type, true, out PropertyType parsedType))
                Type = parsedType;
        }

        public static TypeField Create(Guid interactionTypeId, string name, string type = "Text")
            => new(interactionTypeId, name, type);

        public void Update(string name, string type)
        {
            Name = name;
            if (Enum.TryParse<PropertyType>(type, true, out PropertyType parsedType))
                Type = parsedType;
        }

    }
}