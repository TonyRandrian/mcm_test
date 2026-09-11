using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Catalog.Application.Features.ServiceCategories.Commands.DeleteServiceCategory
{
    public class DeleteServiceCategoryCommand
        : IRequest<ApiResponse<DeleteServiceCategoryResponse>>
    {
        public Guid Id { get; set; }
        public bool Force { get; set; } = false;
    }
}