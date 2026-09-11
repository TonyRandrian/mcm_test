using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Shared.Domain.Primitives
{
    public abstract class AggregateRoot : AuditableEntity
    {
        private List<IDomainEvent> _domainEvents = [];
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

        public void RaiseDomainEvent(IDomainEvent @event)
        {
            _domainEvents ??= [];
            _domainEvents.Add(@event);
        }

        public void ClearDomainEvent()
        {
            _domainEvents.Clear();
        }
    }
}