namespace Mcm.Authorizations.Application.Features.Roles.Commands.DeleteRole
{
    public record DeleteRoleRequest
    (
        bool Force = false
    );
}