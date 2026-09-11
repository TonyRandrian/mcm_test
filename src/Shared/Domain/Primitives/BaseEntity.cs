using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Shared.Domain.Primitives
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected init; } = Guid.NewGuid();
    }

}
