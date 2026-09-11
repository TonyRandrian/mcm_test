using System.IdentityModel.Tokens.Jwt;
using Mcm.Company.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Shared.Infrastructure.Autorisations
{
    public class AuthorizationRequirementHandler(IServiceScopeFactory serviceScopeFactory)
        : AuthorizationHandler<AuthorizationRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;


        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationRequirement requirement)
        {
            var teamMemberId = context.User.Claims.FirstOrDefault(
                c => c.Type == JwtRegisteredClaimNames.Aud)?.Value;
            if (!Guid.TryParse(teamMemberId, out Guid teamMemberGuid))
                return;
            
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            IPermissionService permissionService = scope.ServiceProvider
                .GetRequiredService<IPermissionService>();

            HashSet<string> allowedpermissions = await permissionService.GetPermissions(teamMemberGuid);
            
            if (allowedpermissions.Contains(requirement.Permission))
                context.Succeed(requirement);

        }
    }
}