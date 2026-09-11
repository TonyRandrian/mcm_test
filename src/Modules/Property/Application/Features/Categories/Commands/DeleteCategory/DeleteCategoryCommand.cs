using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Property.Application.Features.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand : IRequest<ApiResponse<DeleteCategoryResponse>>
    {
        public Guid Id { get; set; }
        public bool Force { get; set; } = false;
    }
}