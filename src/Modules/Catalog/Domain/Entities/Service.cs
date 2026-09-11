using Mcm.Shared.Domain.Exceptions;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Catalog.Domain.Entities
{
    public class Service : AuditableEntity, ITenantScoped
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal? MaxPrice { get; private set; }
        public decimal? MinPrice { get; private set; }
        public string Unit { get; private set; } = string.Empty;
        public Guid? CategoryId { get; private set; }
        public Guid CurrencyId { get; private set; }
        public Guid TenantId { get; set; }
        public Guid CompanyId { get; set; }
        public Resource CoverPicture { get; private set; } = null!;
        public Currency Currency { get; private set; } = null!;
        public ServiceCategory? Category { get; private set; } = null!;
        private readonly List<Resource> _images = new();
        public IReadOnlyList<Resource> Images => _images.AsReadOnly();

        private Service() { }
        private Service(Guid companyId, string name, string description, decimal? minPrice, decimal? maxPrice, string unit, Guid currencyId, Guid? categoryId)
        {
            CompanyId = companyId;
            Name = name;
            Description = description;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            Unit = unit;
            CurrencyId = currencyId;
            CategoryId = categoryId;
        }

        public static Service Create(Guid companyId, string name, string description, decimal? minPrice, decimal? maxPrice, string unit, Guid currencyId, Guid? categoryId)
        {
            if (maxPrice < minPrice) 
                throw new InvalidOperationException("maxPrice must be greater than minPrice");
            return new(companyId, name, description, minPrice, maxPrice, unit, currencyId, categoryId);
        }

        public void Update(string name, string description, decimal minPrice, decimal maxPrice, string unit, Guid currencyId, Guid? categoryId)
        {
            Name = name;
            Description = description;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            Unit = unit;
            CurrencyId = currencyId;
            CategoryId = categoryId;
        }

        public void UpdateCoverPicture(Resource coverPicture)
        {
            CoverPicture = coverPicture;
        }

        public void AddImage(Resource image)
        {
            if (_images.FirstOrDefault(i => i.Url == image.Url) is not null)
                return;
            _images.Add(image);
        }

        public void RemoveImage(Resource image)
        {
            if (_images.FirstOrDefault(i => i.Url == image.Url) is null)
                return;
            _images.Remove(image);
        }
    }
}