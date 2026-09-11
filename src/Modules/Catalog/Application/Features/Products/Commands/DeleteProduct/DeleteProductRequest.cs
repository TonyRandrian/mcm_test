namespace Mcm.Catalog.Application.Features.Products.Commands.DeleteProduct
{
    public record DeleteProductRequest
    (
        bool Force = false
    );
}