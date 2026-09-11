using Mcm.Catalog.Application.Features.Services.Commands.CreateService;
using Mcm.Catalog.Application.Features.Services.Commands.DeleteService;
using Mcm.Catalog.Application.Features.Services.Commands.UpdateService;
using Mcm.Catalog.Application.Features.Services.Queries.GetAllService;
using Mcm.Catalog.Application.Features.Services.Queries.GetService;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Catalog.Presentation.Controllers;

[ApiController]
[Route("api/services")]
public class ServiceController(IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<GetAllServiceResponse>>> GetAll(
        [FromQuery] GetAllServiceRequest request)
    {
        var result = await _mediator.Send(new GetAllServiceQuery(request));
        return Ok(result);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<ApiResponse<GetServiceResponse>>> GetById(
        Guid Id)
    {
        var result = await _mediator.Send(new GetServiceQuery(
            new GetServiceRequest(Id)));
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateServiceResponse>>> Create(
        [FromForm] CreateServiceRequest body)
    {
        var result = await _mediator.Send(new CreateServiceCommand
        {
            CategoryId = body.CategoryId,
            Description = body.Description,
            Name = body.Name,
            Unit = body.Unit,
            MaxPrice = body.MaxPrice,
            MinPrice = body.MinPrice,
            Images = body.Images,
            CoverPicture = body.CoverPicture,
            CurrencyId = body.CurrencyId
        });
        return Ok(result);
    }

    [HttpPut("{Id}")]
    public async Task<ActionResult<ApiResponse<UpdateServiceResponse>>> Update(
        Guid Id,
        [FromForm] UpdateServiceRequest body)
    {
        var result = await _mediator.Send(new UpdateServiceCommand
        {
            Id = Id,
            CategoryId = body.CategoryId,
            Description = body.Description,
            Name = body.Name,
            Unit = body.Unit,
            MinPrice = body.MinPrice,
            MaxPrice = body.MaxPrice,
            Images = body.Images,
            NewImages = body.NewImages,
            CoverPicture = body.CoverPicture,
            CurrencyId = body.CurrencyId
        });
        return Ok(result);
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult<ApiResponse<DeleteServiceResponse>>> Delete(
        Guid Id,
        [FromQuery] DeleteServiceRequest request)
    {
        var result = await _mediator.Send(new DeleteServiceCommand
        {
            Id = Id,
            Force = request.Force
        });
        return Ok(result);
    }
}