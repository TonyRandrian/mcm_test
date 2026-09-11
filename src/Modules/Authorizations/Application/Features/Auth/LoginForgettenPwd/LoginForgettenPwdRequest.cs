namespace Mcm.Authorizations.Application.Features.Auth.LoginForgettenPwd
{
    public record LoginForgettenPwdRequest
    (
        string Email,
        string Token
    );
}