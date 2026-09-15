using Mcm.Interactions.Domain.Events;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Interactions.Domain.Entities
{
    public class InteractionType : AggregateRoot, ITenantScoped
    {
        public Name Title { get; private set; } = null!;
        public string LabelColor { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public Guid TenantId { get; set; }
        public Guid? ParentId { get; set; }
        public InteractionType? Parent { get; set; } = null!;
        private List<TypeField> _fields { get; set; } = new();
        public IReadOnlyList<TypeField> Fields => _fields;


        private InteractionType() {}
        private InteractionType(string title, string labelColor, string? description, Guid? parentId)
        {
            Title = title;
            LabelColor = labelColor;
            Description = description;
            ParentId = parentId;
        }

        public static InteractionType Create(string title, string labelColor, string? description, Guid? parentId)
        {
            InteractionType type = new(title, labelColor, description, parentId);
            type.RaiseDomainEvent(new InteractionTypeCreatedEvent(type.Id, type.Title));
            return type;
        }

        public void Update(string title, string labelColor, string? description)
        {
            Title = title;
            LabelColor = labelColor;
            Description = description;
            RaiseDomainEvent(new InteractionTypeUpdatedEvent(Id, Title));
        }

        public override void Delete()
        {
            base.Delete();
            RaiseDomainEvent(new InteractionTypeDeletedEvent(Id, Title));
        }
    }
}