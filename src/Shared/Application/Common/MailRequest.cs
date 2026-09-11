namespace Mcm.Shared.Application.Common
{
    public record MailInvitationRequest
    (
        string FirstName,
        string LastName,
        string Email,
        string CompanyName,
        string Token
    );
}