namespace Mcm.Authorizations.Application.Features.TeamMembers.Commands.SwitchCompany
{
    public record SwitchCompanyRequest
    (
        string RefreshToken,
        Guid CompanyId
    );
}