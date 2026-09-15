using Mcm.Authorizations.Application.Features.TeamMembers.Commands.SwitchCompany;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Authorizations.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/auth")]
    public class SwitchCompanyController(IMediator mediator)
        : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("switchCompany")]
        public async Task<ActionResult<ApiResponse<SwitchCompanyResponse>>> SwitchCompany(
            [FromBody] SwitchCompanyRequest body)
        {
            var res = await _mediator.Send(new SwitchCompanyCommand
            {
                CompanyId = body.CompanyId,
                RefreshToken = body.RefreshToken
            });
            return Ok(res);
        }

    }
}
