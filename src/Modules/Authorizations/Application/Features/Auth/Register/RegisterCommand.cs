using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Authorizations.Application.Features.Auth.Register
{
    public class RegisterCommand
        : IRequest<ApiResponse<RegisterResponse>>
    {
        public string Name { get; set; } = string.Empty;
        public string Acronym { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile Logo { get; set; } = null!;
        public List<CompanyPrincipalValue>? Values { get; set; } = [];

        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        // public Guid CompanyId { get; set; }
    }

    public class CompanyPrincipalValue
    {
        public Guid PropertyId { get; set; }
        public string Value { get; set; } = string.Empty;        
    }
}