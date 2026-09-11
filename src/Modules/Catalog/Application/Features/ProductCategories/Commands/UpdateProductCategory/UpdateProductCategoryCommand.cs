using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Catalog.Application.Features.ProductCategories.Commands.UpdateProductCategory
{
    public class UpdateProductCategoryCommand
        : IRequest<ApiResponse<UpdateProductCategoryResponse>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        
        public string Name { get; set; } = string.Empty;
    }
}