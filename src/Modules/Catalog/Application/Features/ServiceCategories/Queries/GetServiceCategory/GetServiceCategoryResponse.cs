using Mcm.Shared.Application.Common;

namespace Mcm.Catalog.Application.Features.ServiceCategories.Queries.GetServiceCategory
{
    public class GetServiceCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}  