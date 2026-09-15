using Microsoft.AspNetCore.Authorization;

namespace Mcm.Shared.Infrastructure.Autorisations
{
    public class AuthorizationRequirement(
        string permission)
        : IAuthorizationRequirement
    {
        public string Permission { get; } = permission;
    }
}