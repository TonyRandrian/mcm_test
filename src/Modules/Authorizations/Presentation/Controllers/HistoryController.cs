using Mcm.Authorizations.Application.Features.History.Queries.GetActivityLogs;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Authorizations.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/history")]
    public class HistoryController(IMediator mediator)
        : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<ApiResponse<GetActivityLogsResponse>>> GetActivityLogs(
            [FromQuery] GetActivityLogsRequest request)
        {
            var res = await _mediator.Send(new GetActivityLogsQuery(request));
            return Ok(res);
        }
    
    }
}
