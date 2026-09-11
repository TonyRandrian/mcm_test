using System.Text.Json;
using Mcm.Authorizations.Application.Features.Auth.CheckEmail;
using Mcm.Authorizations.Application.Features.Auth.Login;
using Mcm.Authorizations.Application.Features.Auth.LoginForgettenPwd;
using Mcm.Authorizations.Application.Features.Auth.Register;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Authorizations.Presentation.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IMediator mediator)
        : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginResponse>>> Login(
            [FromBody] LoginRequest body)
        {
            var res = await _mediator.Send(new LoginCommand
            {
                Email = body.Email,
                Password = body.Password
            });
            return Ok(res);
        }

        [HttpPost("login-forget-password")]
        public async Task<ActionResult<ApiResponse<LoginForgettenPwdResponse>>> LoginForgetPassword(
            [FromBody] LoginForgettenPwdRequest body)
        {
            var res = await _mediator.Send(new LoginForgettenPwdCommand
            {
                Email = body.Email,
                Token = body.Token
            });
            return Ok(res);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<RegisterResponse>>> Register(
            [FromForm] RegisterRequest body)
        {
            List<CompanyPrincipalValue>? values = null;
            if (body.Values is not null)
                values = JsonSerializer.Deserialize<List<CompanyPrincipalValue>>(body.Values, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            
            var res = await _mediator.Send(new RegisterCommand
            {
                Name = body.Name,
                Acronym = body.Acronym,
                Description = body.Description,
                Logo = body.Logo,
                Values = values,

                FirstName = body.FirstName,
                LastName = body.LastName,
                Password = body.Password,
                Email = body.Email,
            });
            return Ok(res);
        }

        [HttpPost("check-mail")]
        public async Task<ActionResult<ApiResponse<CheckEmailResponse>>> CheckEmail(
            [FromBody] CheckEmailRequest body)
        {
            var res = await _mediator.Send(new CheckEmailCommand
            {
                Email = body.Email
            });
            return Ok(res);
        }
    }
}
