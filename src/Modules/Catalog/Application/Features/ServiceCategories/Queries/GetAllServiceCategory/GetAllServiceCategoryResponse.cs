using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Catalog.Application.Features.ServiceCategories.Queries.GetAllServiceCategory
{
    public record GetAllServiceCategoryResponse
    (
        List<ServiceCategoryResponse> Categories
    );


    public class ServiceCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Count { get; set; }
        public List<ServiceCategoryResponse> SubCategories { get; set; } = [];
    }
    
}