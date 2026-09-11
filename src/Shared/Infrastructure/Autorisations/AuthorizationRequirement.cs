using Microsoft.AspNetCore.Authorization;

namespace Mcm.Shared.Infrastructure.Autorisations
{
    public class AuthorizationRequirement : IAuthorizationRequirement
    {

        public AuthorizationRequirement(string permission)
        {
            Permission = permission;
        }
        public string Permission { get; }
    }
}