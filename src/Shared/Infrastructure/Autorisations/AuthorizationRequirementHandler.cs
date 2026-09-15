using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Mcm.Shared.Infrastructure.Autorisations
{
    public class AuthorizationRequirementHandler(IServiceScopeFactory serviceScopeFactory)
        : AuthorizationHandler<AuthorizationRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AuthorizationRequirement requirement)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            // var teamMemberToken = context.User.Claims.FirstOrDefault(
            //     c => c.Type == JwtRegisteredClaimNames.Aud)?.Value;
            // if (!Guid.TryParse(teamMemberToken, out Guid teamMemberId))
            //     return;
            ICurrentUserService currentUserService = scope.ServiceProvider
                .GetRequiredService<ICurrentUserService>();
            IPermissionService permissionService = scope.ServiceProvider
                .GetRequiredService<IPermissionService>();

            HashSet<string> allowedpermissions = await permissionService.GetPermissions(currentUserService.TeamMemberId, currentUserService.CompanyId);           
            if (allowedpermissions.Contains(requirement.Permission))
                context.Succeed(requirement);

        }
    }
}