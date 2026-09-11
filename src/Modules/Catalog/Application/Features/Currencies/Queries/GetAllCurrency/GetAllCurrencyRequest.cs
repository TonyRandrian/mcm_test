namespace Mcm.Catalog.Application.Features.Currencies.Queries.GetAllCurrency
{
    public record GetAllCurrencyRequest
    (
        string? SearchName = null, 
        int Page = 1, 
        int Limit = 5
    );
}