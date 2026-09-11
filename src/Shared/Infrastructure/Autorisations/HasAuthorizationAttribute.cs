using Mcm.Shared.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Mcm.Shared.Infrastructure.Autorisations
{
    public class HasAuthorizationAttribute : AuthorizeAttribute
    {
        public HasAuthorizationAttribute(PermissionsEnum permission)
            : base(policy: permission.ToString())
        {
            
        }    
    }
}