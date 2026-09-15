namespace Mcm.Property.Application.Features.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryResponse
    {
        public List<CategoryResponse> Categories { get; set; } = [];
    }


    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<PropertyResponse> Properties { get; set; } = [];
    }
    
    public class PropertyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool IsMultiple { get; set; }
        public bool IsRequired { get; set; }
        public bool IsSensitive { get; set; }
    }
}