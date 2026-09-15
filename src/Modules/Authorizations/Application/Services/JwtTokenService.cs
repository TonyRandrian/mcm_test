using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Mcm.Authorizations.Application.Interfaces;
using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Mcm.Authorizations.Application.Services;

public class JwtTokenService
    : IJwtTokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey key;
    private readonly string issuer;
    private readonly string audience;
    private readonly SigningCredentials cred;
 
    public JwtTokenService(IConfiguration config)
    {
        _config = config;
        key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]!));
        cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        issuer = _config["JwtSettings:Issuer"] ?? throw new ArgumentNullException("JwtSettings:Issuer");
        audience = _config["JwtSettings:Audience"] ?? throw new ArgumentNullException("JwtSettings:Audience");
    }

    public async Task<string> GenerateInvitationToken(Guid teamMemberId, Guid companyId)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, companyId.ToString()),
            new Claim("team_member_id", teamMemberId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var expiryDate = DateTime.UtcNow.AddDays(7);
        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims: claims,
            expires: expiryDate,
            signingCredentials: cred);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> GenerateToken(TeamMember teamMember, Guid? companyId = null)
    {
        var valueCompanyId = companyId ?? teamMember.CompanyId;
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, valueCompanyId.ToString()),
            new("team_member_id", teamMember.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("tenant_id", teamMember.TenantId.ToString()),
        };

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["JwtSettings:ExpiryMinutes"] ?? "60")),
            signingCredentials: cred);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ObjectToken VerifyToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token);
        var tokenSecurity = jsonToken as JwtSecurityToken;
        
        if (!Guid.TryParse(tokenSecurity?.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value, out Guid company))
            throw new BadRequestException("Parsing to CompanyId Error");
        if (!Guid.TryParse(tokenSecurity?.Claims.First(claim => claim.Type == "team_member_id").Value, out Guid teamMember))
            throw new BadRequestException("Parsing to TeamMemberId Error");
        
        var tenantClaim = tokenSecurity?.Claims.FirstOrDefault(claim => claim.Type == "tenant_id")?.Value;
        return new ObjectToken
        {
            TeamMemberId = teamMember,
            CompanyId = company,
            TenantId = (tenantClaim is not null && Guid.TryParse(tenantClaim, out Guid tenant)) ? tenant : (Guid?)null
        };
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
