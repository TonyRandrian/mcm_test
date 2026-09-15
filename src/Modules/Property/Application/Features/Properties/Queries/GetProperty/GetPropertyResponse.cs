namespace Mcm.Property.Application.Features.Properties.Queries.GetProperty
{
    public class GetPropertyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsRequired { get; set; }
        public bool IsMultiple { get; set; }
        public bool IsSensitive { get; set; }
    }
}