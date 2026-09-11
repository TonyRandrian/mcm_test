using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Mcm.Shared.Application.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContext) : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContext = httpContext;
        private const string TokenStringHeader = "Authorization";

        private ClaimsPrincipal User =>
            _httpContext.HttpContext?.User
            ?? throw new UnauthorizedAccessException("User not authenticated");

        public Guid TenantId => GetGuidClaim("tenant_id");
        public Guid TeamMemberId => GetGuidClaim(JwtRegisteredClaimNames.Aud);
        public Guid CompanyId => GetGuidClaim(JwtRegisteredClaimNames.Sub);

        private Guid GetGuidClaim(string claimType)
    {
        var token = GetToken();

        if (string.IsNullOrWhiteSpace(token))
            return Guid.Empty;

        var handler = new JwtSecurityTokenHandler();

        var jwt = handler.ReadJwtToken(token);

        var value = jwt.Claims
            .FirstOrDefault(x => x.Type == claimType)
            ?.Value;

        return Guid.TryParse(value, out var guid)
            ? guid
            : Guid.Empty;
    }

    private string? GetToken()
    {
        var authorization = _httpContext.HttpContext?
            .Request.Headers[TokenStringHeader]
            .ToString();

        if (string.IsNullOrWhiteSpace(authorization))
            return null;

        if (!authorization.StartsWith("Bearer "))
            return null;

        return authorization["Bearer ".Length..];
    }

        // public string GetTokenString()
        // {
        //     var Token = (_httpContext.HttpContext?.Request.Headers[TokenStringHeader]).ToString();
        //     if (string.IsNullOrEmpty(Token)) return;
        //     var token = Token.Split(" ")[0] == "Bearer"
        //             ? Token.Split(" ")[1]
        //             : throw new ArgumentException("Token invalid");
        //     if (string.IsNullOrEmpty(token)) return;
        //     var handler = new JwtSecurityTokenHandler();
        //     var jsonToken = handler.ReadToken(token);
        //     var tokenSecurity = jsonToken as JwtSecurityToken;
        //     return tokenSecurity;
        //     if (!Guid.TryParse(tokenSecurity?.Claims.First(claim => claim.Type == "sub").Value, out Guid companyId))
        //         throw new ArgumentException("Parsing to TenantId Error");
        //     if (!Guid.TryParse(tokenSecurity?.Claims.First(claim => claim.Type == "aud").Value, out Guid tmId))
        //         throw new ArgumentException("Parsing to TenantId Error");
        //     if (!Guid.TryParse(tokenSecurity?.Claims.First(claim => claim.Type == "tenant_id").Value, out Guid tenantId))
        //         throw new ArgumentException("Parsing to TenantId Error");
        //     TeamMemberId = tmId;
        //     CompanyId = companyId;
        //     TenantId = tenantId;
            
        // }
    }
}