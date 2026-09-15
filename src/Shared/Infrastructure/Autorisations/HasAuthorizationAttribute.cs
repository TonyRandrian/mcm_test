using Mcm.Shared.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Mcm.Shared.Infrastructure.Autorisations
{
    public class HasAuthorizationAttribute
        : AuthorizeAttribute
    {
        public HasAuthorizationAttribute(PermModule module, PermAction action)
            : base(policy: $"{module.ToString()}:{action.ToString()}")
        {
        }    
    }
}