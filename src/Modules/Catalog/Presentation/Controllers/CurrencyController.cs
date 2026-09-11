using Mcm.Catalog.Application.Features.Currencies.Queries.GetAllCurrency;
using Mcm.Catalog.Application.Features.ProductCategories.Queries.GetAllProductCategory;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Catalog.Presentation.Controllers;

[ApiController]
[Route("api/currencies")]
public class CurrencyController(IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<GetAllCurrencyResponse>>> GetAll(
        [FromQuery] GetAllCurrencyRequest request)
    {
        var result = await _mediator.Send(new GetAllCurrencyQuery(request));
        return Ok(result);
    }

}