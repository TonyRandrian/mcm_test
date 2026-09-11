namespace Mcm.Authorizations.Application.Features.Auth.Login
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public Guid CompanyId { get; set; }
    }
}