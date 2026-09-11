using Mcm.Shared.Application.Modules.DTOs;

namespace Mcm.Shared.Application.Modules
{
    public interface IContactModule
    {  
        Task<List<IdentityDto>> GetIdentities(List<Guid> contactIds);  
    }
}