using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Property.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<ApiResponse<UpdateCategoryResponse>>
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<string>? EntityTypes { get; set; }
    }
}