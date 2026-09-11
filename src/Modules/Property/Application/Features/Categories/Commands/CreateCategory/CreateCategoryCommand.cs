using System.Runtime.Serialization;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.Interfaces;
using MediatR;

namespace Mcm.Property.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<ApiResponse<CreateCategoryResponse>>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string[] EntityTypes { get; set; } = [];
    }
}