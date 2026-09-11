using Mcm.Catalog.Application.Features.ServiceCategories.Commands.CreateServiceCategory;
using Mcm.Catalog.Application.Features.ServiceCategories.Commands.DeleteServiceCategory;
using Mcm.Catalog.Application.Features.ServiceCategories.Commands.UpdateServiceCategory;
using Mcm.Catalog.Application.Features.ServiceCategories.Queries.GetAllServiceCategory;
using Mcm.Catalog.Application.Features.ServiceCategories.Queries.GetServiceCategory;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Catalog.Presentation.Controllers;

[ApiController]
[Route("api/serviceCategories")]
public class ServiceCategoryController(IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<GetAllServiceCategoryResponse>>> GetAll(
        [FromQuery] GetAllServiceCategoryRequest request)
    {
        var result = await _mediator.Send(new GetAllServiceCategoryQuery(request));
        return Ok(result);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<ApiResponse<GetServiceCategoryResponse>>> GetById(
        Guid Id)
    {
        var result = await _mediator.Send(new GetServiceCategoryQuery(
            new GetServiceCategoryRequest(Id)));
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateServiceCategoryResponse>>> Create(
        [FromBody] CreateServiceCategoryRequest body)
    {
        var result = await _mediator.Send(new CreateServiceCategoryCommand
        {
            Name = body.Name,
            ParentCategoryId = body.ParentCategoryId
        });
        return Ok(result);
    }

    [HttpPut("{Id}")]
    public async Task<ActionResult<ApiResponse<UpdateServiceCategoryResponse>>> Update(
        Guid Id,
        [FromBody] UpdateServiceCategoryRequest body)
    {
        var result = await _mediator.Send(new UpdateServiceCategoryCommand
        {
            Id = Id,
            Name = body.Name
        });
        return Ok(result);
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult<ApiResponse<DeleteServiceCategoryResponse>>> Delete(
        Guid Id,
        [FromQuery] DeleteServiceCategoryRequest request)
    {
        var result = await _mediator.Send(new DeleteServiceCategoryCommand
        {
            Id = Id,
            Force = request.Force
        });
        return Ok(result);
    }
}