using Mcm.Property.Application.Features.Categories.Commands.CreateCategory;
using Mcm.Property.Application.Features.Categories.Commands.DeleteCategory;
using Mcm.Property.Application.Features.Categories.Commands.SetCategoryVisibility;
using Mcm.Property.Application.Features.Categories.Commands.UpdateCategory;
using Mcm.Property.Application.Features.Categories.Queries.GetAllCategory;
using Mcm.Property.Application.Features.Categories.Queries.GetCategory;
using Mcm.Property.Application.Features.Properties.Commands.CreateProperty;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Infrastructure.Autorisations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Property.Presentation.Controllers;

[ApiController]
[Route("api/category")]
public class CategoryController(IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HasAuthorization(PermModule.Category, PermAction.Create)]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateCategoryResponse>>> Create(
        [FromBody] CreateCategoryRequest body)
    {
        var result = await _mediator.Send(new CreateCategoryCommand
        {
            Name = body.Name,
            Description = body.Description,
            EntityTypes = body.EntityTypes,
        });
        return CreatedAtAction(nameof(Create), result);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<GetAllCategoryResponse>>> GetAll(
        [FromQuery] GetAllCategoriesRequest request)
    {
        var result = await _mediator.Send(
            new GetAllCategoryQuery(request));
        return Ok(result);
    }

    [HasAuthorization(PermModule.Category, PermAction.Read)]
    [HttpGet("{Id}")]
    public async Task<ActionResult<ApiResponse<GetCategoryResponse>>> GetById(
        Guid Id)
    {
        var result = await _mediator.Send(new GetCategoryQuery(new GetCategoryRequest(Id)));
        return Ok(result);
    }

    [HasAuthorization(PermModule.Category, PermAction.Update)]
    [HttpPut("{Id}")]
    public async Task<ActionResult<ApiResponse<UpdateCategoryResponse>>> Update(
        Guid Id, [FromBody] UpdateCategoryRequest body)
    {
        var result = await _mediator.Send(new UpdateCategoryCommand
        {
            Id = Id,
            Name = body.Name,
            Description = body.Description,
            EntityTypes = body.EntityTypes,
        });
        return Ok(result);
    }

    [HasAuthorization(PermModule.Category, PermAction.Update)]
    [HttpPut("{Id}/visibility")]
    public async Task<ActionResult<ApiResponse<SetCategoryVisibilityResponse>>> UpdateVisibility(
        Guid Id, [FromBody] SetCategoryVisibilityRequest body)
    {
        var result = await _mediator.Send(new SetCategoryVisibilityCommand
        {
            CategoryId = Id,
            EntityType = body.EntityType,
            IsVisibleInProfile = body.IsVisibleInProfile
        });
        return Ok(result);
    }

    [HasAuthorization(PermModule.Category, PermAction.Delete)]
    [HttpDelete("{Id}")]
    public async Task<ActionResult<ApiResponse<DeleteCategoryResponse>>> Delete(
        Guid Id, [FromQuery] DeleteCategoryRequest request)
    {
        var result = await _mediator.Send(new DeleteCategoryCommand
        {
            Id = Id,
            Force = request.Force
        });
            
        return Ok(result);
    }

    [HasAuthorization(PermModule.Category, PermAction.Create)]
    [HttpPost("{Id}/property")]
    public async Task<ActionResult<ApiResponse<CreatePropertyResponse>>> CreateProperty(
        Guid Id, [FromBody] CreatePropertyRequest body)
    {
        var result = await _mediator.Send(new CreatePropertyCommand
        {
            CategoryId = Id,
            Name = body.Name,
            Description = body.Name,
            Type = body.Type,
            IsMultiple = body.IsMultiple,
            IsRequired = body.IsRequired,
            IsSensitive = body.IsSensitive
        });
        return Ok(result);
    }

}
