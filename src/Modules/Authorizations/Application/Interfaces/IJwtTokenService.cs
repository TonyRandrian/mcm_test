using Mcm.Authorizations.Domain.Entities;

namespace Mcm.Authorizations.Application.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string> GenerateToken(TeamMember teamMember);
        Task<string> GenerateInvitationToken(Guid teamMemberId, Guid companyId);
        ObjectToken VerifyToken(string token);
    }
    
    public class ObjectToken
    {
        public Guid TeamMemberId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? TenantId { get; set; }
        
    }
}


