using Mcm.Shared.Domain.Primitives;

namespace Mcm.Authorizations.Domain.Entities
{
    public class RefreshToken
        : BaseEntity
    {
        public Guid TeamMemberId { get; set; }
        public Guid CompanyId { get; set; }
        public string Token { get; set; } = string.Empty;
        public bool IsUsed { get; private set; }
        public DateTime UsedAt { get; private set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public TeamMember TeamMember { get; set; } = null!;

        private RefreshToken() {}
        private RefreshToken(string token, Guid teamMemberId, Guid companyId)
        {
            Token = token;
            TeamMemberId = teamMemberId;
            CompanyId = companyId;
            ExpiresAt = DateTime.UtcNow.AddDays(3);
            IsUsed = false;
        }

        public static RefreshToken Create(string token, Guid teamMemberId, Guid companyId)
        => new(token, teamMemberId, companyId);

        public bool IsExpired => DateTime.UtcNow > ExpiresAt || IsUsed;

        public void MarkAsUsed() => IsUsed = true;
    }
}