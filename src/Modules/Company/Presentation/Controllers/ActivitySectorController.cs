using Mcm.Company.Application.Features.ActivitySectors.Queries.GetAllActivitySector;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Infrastructure.Autorisations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Company.Presentation.Controllers;

[ApiController]
[Route("api/activitySectors")]
public class ActivitySectorController(IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<GetAllActivitySectorResponse>>> GetAll
        ([FromQuery] GetAllActivitySectorRequest request)
    {
        var res = await _mediator.Send(new GetAllActivitySectorQuery(request));
        return Ok(res);
    }

}