using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Property.Application.Features.Categories.Commands.SetCategoryVisibility
{
    public class SetCategoryVisibilityCommand
        : IRequest<ApiResponse<SetCategoryVisibilityResponse>>
    {
        public Guid CategoryId { get; set; }
        public string EntityType { get; set; } = string.Empty;
        public bool IsVisibleInProfile { get; set; }
    }
}