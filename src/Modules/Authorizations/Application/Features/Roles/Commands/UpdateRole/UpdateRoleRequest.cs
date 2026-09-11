namespace Mcm.Authorizations.Application.Features.Roles.Commands.UpdateRole
{
    public record UpdateRoleRequest
    (
        string? Title,
        string? Description,
        List<Guid>? Companies,
        IEnumerable<(string Module, string Action)>? Permissions
    );
}