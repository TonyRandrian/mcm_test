namespace Mcm.Authorizations.Application.Features.Roles.Commands.CreateRole
{
    public record CreateRoleRequest
    (
        string Title,
        string Description,
        List<Guid> Companies,
        IEnumerable<CreateRole_Permission> Permissions
    );

    public record CreateRole_Permission(string Module, string Action);
}