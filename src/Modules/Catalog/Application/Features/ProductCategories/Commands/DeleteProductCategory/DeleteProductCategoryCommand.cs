using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Catalog.Application.Features.ProductCategories.Commands.DeleteProductCategory
{
    public class DeleteProductCategoryCommand
        : IRequest<ApiResponse<DeleteProductCategoryResponse>>
    {
        public Guid Id { get; set; }
        public bool Force { get; set; } = false;
    }
}