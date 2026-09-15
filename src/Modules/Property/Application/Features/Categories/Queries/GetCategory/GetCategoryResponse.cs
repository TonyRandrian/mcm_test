namespace Mcm.Property.Application.Features.Categories.Queries.GetCategory
{
    public class GetCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<CategoryPropertyResponse>? Properties { get; set; } = [];
    }

    public class CategoryPropertyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool IsRequired { get; set; }
        public bool IsMultiple { get; set; }
        public bool IsSensitive { get; set; }
    }
}