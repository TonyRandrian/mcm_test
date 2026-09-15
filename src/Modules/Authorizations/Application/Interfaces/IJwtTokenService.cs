using Mcm.Authorizations.Domain.Entities;

namespace Mcm.Authorizations.Application.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string> GenerateToken(TeamMember teamMember, Guid? companyId = null);
        Task<string> GenerateInvitationToken(Guid teamMemberId, Guid companyId);
        string GenerateRefreshToken();
        ObjectToken VerifyToken(string token);
    }
    
    public class ObjectToken
    {
        public Guid TeamMemberId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? TenantId { get; set; }
        
    }
}


