using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Catalog.Application.Features.Currencies.Queries.GetAllCurrency
{
    public record GetAllCurrencyResponse
    (
        List<CurrencyResponse> Currencies
    );


    public class CurrencyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
    }
    
}