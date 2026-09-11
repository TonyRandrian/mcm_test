using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Catalog.Application.Features.ServiceCategories.Commands.UpdateServiceCategory
{
    public class UpdateServiceCategoryCommand
        : IRequest<ApiResponse<UpdateServiceCategoryResponse>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        
        public string Name { get; set; } = string.Empty;
    }
}