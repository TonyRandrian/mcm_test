namespace Mcm.Authorizations.Application.Features.Roles.Commands.CreateRole
{
    public record CreateRoleRequest
    (
        string Title,
        string Description,
        List<Guid> Companies,
        IEnumerable<(string Module, string Action)> Permissions
    );
}