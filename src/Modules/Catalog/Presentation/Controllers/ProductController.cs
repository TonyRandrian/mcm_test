using Mcm.Catalog.Application.Features.Products.Commands.CreateProduct;
using Mcm.Catalog.Application.Features.Products.Commands.DeleteProduct;
using Mcm.Catalog.Application.Features.Products.Commands.UpdateProduct;
using Mcm.Catalog.Application.Features.Products.Queries.GetAllProduct;
using Mcm.Catalog.Application.Features.Products.Queries.GetProduct;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Infrastructure.Autorisations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Catalog.Presentation.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<GetAllProductResponse>>> GetAll(
        [FromQuery] GetAllProductRequest request)
    {
        var result = await _mediator.Send(new GetAllProductQuery(request));
        return Ok(result);
    }

    [HasAuthorization(PermModule.Product, PermAction.Read)]
    [HttpGet("{Id}")]
    public async Task<ActionResult<ApiResponse<GetProductResponse>>> GetById(
        Guid Id)
    {
        var result = await _mediator.Send(new GetProductQuery(
            new GetProductRequest(Id)));
        return Ok(result);
    }

    [HasAuthorization(PermModule.Product, PermAction.Create)]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateProductResponse>>> Create(
        [FromForm] CreateProductRequest body)
    {
        var result = await _mediator.Send(new CreateProductCommand
        {
            CategoryIds = body.CategoryIds,
            Description = body.Description,
            Name = body.Name,
            Unit = body.Unit,
            Price = body.Price,
            Images = body.Images,
            CoverPicture = body.CoverPicture,
            CurrencyId = body.CurrencyId
        });
        return Ok(result);
    }

    [HasAuthorization(PermModule.Product, PermAction.Update)]
    [HttpPut("{Id}")]
    public async Task<ActionResult<ApiResponse<UpdateProductResponse>>> Update(
        Guid Id,
        [FromForm] UpdateProductRequest body)
    {
        var result = await _mediator.Send(new UpdateProductCommand
        {
            Id = Id,
            Name = body.Name,
            Description = body.Description,
            Unit = body.Unit,
            Price = body.Price,
            CoverPicture = body.CoverPicture,
            Images = body.Images,
            NewImages = body.NewImages,
            CurrencyId = body.CurrencyId,
            CategoryIds = body.CategoryIds,
        });
        return Ok(result);
    }

    [HasAuthorization(PermModule.Product, PermAction.Delete)]
    [HttpDelete("{Id}")]
    public async Task<ActionResult<ApiResponse<DeleteProductResponse>>> Delete(
        Guid Id,
        [FromQuery] DeleteProductRequest request)
    {
        var result = await _mediator.Send(new DeleteProductCommand
        {
            Id = Id,
            Force = request.Force
        });
        return Ok(result);
    }
}