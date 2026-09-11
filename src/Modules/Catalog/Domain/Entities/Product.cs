using Mcm.Shared.Domain.Exceptions;
using Mcm.Shared.Domain.Extensions;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Catalog.Domain.Entities
{
    public class Product : AggregateRoot, ITenantScoped
    {
        public string Name
        {
            get;
            private set => field = value.Length < 50 ? value.ToCapitalize() : throw new ArgumentException("Name length must not be greater than 50");
        } = string.Empty;
        public string Description
        {
            get;
            private set => field = value.ToCapitalize();
        } = string.Empty;
        public decimal Price
        { 
            get;
            private set => field = value > 0 ? value : throw new ArgumentException("Price must be greater than zero."); 
        }
        public string Unit { get; private set; } = string.Empty;

        public Guid TenantId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid CoverPictureId { get; set; }
        public Guid CurrencyId { get; private set; }
        public Currency Currency { get; private set; } = null!;
        public Resource CoverPicture { get; private set; } = null!;
        private List<Resource> _images { get; set; } = new();
        public IReadOnlyList<Resource> Images => _images;
        private readonly List<ProductCategoryRelation> _categoryRelations = new();
        public IReadOnlyList<ProductCategoryRelation> CategoryRelations => _categoryRelations.AsReadOnly();

        private Product() { }
        private Product(Guid companyId, string name, string description, decimal price, string unit, Guid currencyId)
        {
            CompanyId = companyId;
            Name = name;
            Description = description;
            Price = price;
            Unit = unit;
            CurrencyId = currencyId;
        }

        public static Product Create(Guid companyId, string name, string description, decimal price, string unit, Guid currencyId)
        {
            return new Product(companyId, name, description, price, unit, currencyId);
        }

        public void Update(string name, string description, decimal price, string unit, Guid currencyId)
        {
            Name = name;
            Description = description;
            Price = price;
            Unit = unit;
            ChangeCurrency(currencyId);
        }

        public void ChangeCurrency(Guid currencyId)
        {
            CurrencyId = currencyId;
            // Currency = null!;
        }
        public void AddImage(Resource image)
        {
            if (_images?.FirstOrDefault(i => i.Url == image.Url) is not null)
                return;
            _images?.Add(image);
        }
        public void RemoveImage(Resource image)
        {
            if (_images?.FirstOrDefault(i => i.Url == image.Url) is null)
                return;
            _images.Remove(image);
        }

        public void UpdateCoverPicture(Resource coverPicture)
        {
            CoverPicture = coverPicture;
        }

        public void AddCategory(ProductCategory category)
        {
            if (_categoryRelations.FirstOrDefault(c => c.CategoryId == category.Id) is not null)
                return;
            _categoryRelations.Add(ProductCategoryRelation.Create(Id, category.Id));
        }

        public void SyncCategories(List<ProductCategory>? categories)
        {
            if (categories is null) return;
            var categoryHash = categories.ToHashSet();
            var toRemove = _categoryRelations.Where(cr =>
                    !categoryHash.Contains(cr.Category));
            foreach (var cr in toRemove)
            {
                _categoryRelations.Remove(cr); 
            }
            
            foreach (var category in categoryHash)
            {
                AddCategory(category);
            }
        }

        public void RemoveCategory(ProductCategory category)
        {
            var exist = _categoryRelations.FirstOrDefault(c => 
                c.CategoryId == category.Id);
            if (exist is null) return;
            _categoryRelations.Remove(exist);
        }
    }
}