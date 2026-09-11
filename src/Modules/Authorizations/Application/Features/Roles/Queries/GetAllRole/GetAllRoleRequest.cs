namespace Mcm.Authorizations.Application.Features.Roles.Queries.GetAllRole
{
    public record GetAllRoleRequest
    (
        int Page = 1,
        int Limit = 5
    );
}