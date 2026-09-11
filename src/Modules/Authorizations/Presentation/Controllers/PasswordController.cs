using Mcm.Authorizations.Application.Features.Auth.Login;
using Mcm.Authorizations.Application.Features.Auth.LoginForgettenPwd;
using Mcm.Authorizations.Application.Features.Password.ForgetPassword;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Company.Presentation.Controllers
{
    [ApiController]
    [Route("api")]
    public class PasswordController(IMediator mediator)
        : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("forget-password")]
        public async Task<ActionResult<ApiResponse<ForgetPasswordResponse>>> ForgetPassword(
            [FromBody] ForgetPasswordRequest body)
        {
            var res = await _mediator.Send(new ForgetPasswordCommand
            {
                Email = body.Email
            });
            return Ok(res);
        }
    
    }
}
