using Mcm.Catalog.Application.Features.ProductCategories.Commands.CreateProductCategory;
using Mcm.Catalog.Application.Features.ProductCategories.Commands.DeleteProductCategory;
using Mcm.Catalog.Application.Features.ProductCategories.Commands.UpdateProductCategory;
using Mcm.Catalog.Application.Features.ProductCategories.Queries.GetAllProductCategory;
using Mcm.Catalog.Application.Features.ProductCategories.Queries.GetProductCategory;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Catalog.Presentation.Controllers;

[ApiController]
[Route("api/productCategories")]
public class ProductCategoryController(IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<GetAllProductCategoryResponse>>> GetAll(
        [FromQuery] GetAllProductCategoryRequest request)
    {
        var result = await _mediator.Send(new GetAllProductCategoryQuery(request));
        return Ok(result);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<ApiResponse<GetProductCategoryResponse>>> GetById(
        Guid Id)
    {
        var result = await _mediator.Send(new GetProductCategoryQuery(
            new GetProductCategoryRequest(Id)));
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateProductCategoryResponse>>> Create(
        [FromBody] CreateProductCategoryRequest body)
    {
        var result = await _mediator.Send(new CreateProductCategoryCommand
        {
            Name = body.Name,
            ParentCategoryId = body.ParentCategoryId
        });
        return Ok(result);
    }

    [HttpPut("{Id}")]
    public async Task<ActionResult<ApiResponse<UpdateProductCategoryResponse>>> Update(
        Guid Id,
        [FromBody] UpdateProductCategoryRequest body)
    {
        var result = await _mediator.Send(new UpdateProductCategoryCommand
        {
            Id = Id,
            Name = body.Name
        });
        return Ok(result);
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult<ApiResponse<DeleteProductCategoryResponse>>> Delete(
        Guid Id,
        [FromQuery] DeleteProductCategoryRequest request)
    {
        var result = await _mediator.Send(new DeleteProductCategoryCommand
        {
            Id = Id,
            Force = request.Force
        });
        return Ok(result);
    }
}