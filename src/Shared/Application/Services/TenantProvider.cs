using Mcm.Shared.Application.Interfaces;

namespace Mcm.Shared.Application.Services
{
    public class TenantProvider(ICurrentUserService currentUserService) : ITenantProvider
    {
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public Guid? GetTenantId()
        {
            return _currentUserService.TenantId;
    
        }
    }
}