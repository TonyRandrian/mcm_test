using Mcm.Shared.Domain.Interfaces;

namespace Mcm.Shared.Domain.Primitives
{
    public abstract class AuditableEntity : BaseEntity, ISoftDeletable
    {
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
        public Guid CreatedBy { get; set; } = Guid.Empty;
        public Guid UpdatedBy { get; set; } = Guid.Empty;
        public Guid DeletedBy { get; set; } = Guid.Empty;
        public DateTime? UpdatedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        public virtual void Delete()
        {
            if (!IsDeleted)
            {
                IsDeleted = true;
                DeletedAt = DateTime.UtcNow;
            }
        }

        public void Restore()
        {
            if (IsDeleted)
            {
                IsDeleted = false;
                DeletedAt = null;
                SetUpdatedAt();
            }
        }

        protected void SetUpdatedAt() => UpdatedAt = DateTime.UtcNow;
    }
}