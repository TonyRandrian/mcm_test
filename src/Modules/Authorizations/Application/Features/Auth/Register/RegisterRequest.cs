using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Authorizations.Application.Features.Auth.Register
{
    public record RegisterRequest
    (
        string Name,
        string Acronym,
        string Description,
        IFormFile Logo,

        string? Values,

        string LastName,
        string FirstName,
        string Email,
        string Password
    );
}