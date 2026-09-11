using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;

namespace Mcm.Authorizations.Domain.Entities
{
    public class PasswordReset : BaseEntity, ITenantScoped
    {
        public Guid TeamMemberId { get; set; }
        public bool IsUsed { get; set; } = false;
        public DateTime ExpiredAt { get; set; }
        public string TokenHashed { get; set; } = string.Empty;
        public Guid TenantId { get; set; }

        private PasswordReset() {}
        private PasswordReset(Guid teamMemberId, string tokenHashed)
        {
            TeamMemberId = teamMemberId;
            ExpiredAt = DateTime.UtcNow.AddMinutes(15);
            TokenHashed = tokenHashed;
        }

        public static PasswordReset Create(Guid teamMemberId, string tokenHashed)
            => new(teamMemberId, tokenHashed);

        public void MakeUsed()
        {
            IsUsed = true;
        }
    }
}