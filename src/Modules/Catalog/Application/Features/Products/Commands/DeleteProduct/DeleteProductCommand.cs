using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Catalog.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand
        : IRequest<ApiResponse<DeleteProductResponse>>
    {
        public Guid Id { get; set; }
        public bool Force { get; set; } = false;
    }
}