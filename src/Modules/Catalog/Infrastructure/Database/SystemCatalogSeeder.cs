using Mcm.Catalog.Domain.Entities;

namespace Mcm.Catalog.Infrastructure.Database
{
    public class SystemCatalogSeeder
    {
        public static async Task SystemDataAsync(CatalogDbContext context)
        {
            if (!context.Currencies.Any())
            {
                var currencies = new List<Currency>
                {
                    Currency.Create("Ariary", "Ar"),
                    Currency.Create("US Dollar", "$"),
                    Currency.Create("Euro", "€")
                };

                context.Currencies.AddRange(currencies);
                await context.SaveChangesAsync();
            }
        }
    }
}