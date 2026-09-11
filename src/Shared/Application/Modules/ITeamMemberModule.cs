using Mcm.Shared.Application.Modules.DTOs;

namespace Mcm.Shared.Application.Modules
{
    public interface ITeamMemberModule
    {
        Task<bool> Exists(Guid tmId);      
        Task<List<IdentityDto>> GetIdentities(List<Guid> tmIds);  
    }
}