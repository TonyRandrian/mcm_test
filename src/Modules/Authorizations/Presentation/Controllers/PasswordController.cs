using Mcm.Authorizations.Application.Features.Password.ForgetPassword;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Authorizations.Presentation.Controllers
{
    [ApiController]
    [AllowAnonymous]
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
