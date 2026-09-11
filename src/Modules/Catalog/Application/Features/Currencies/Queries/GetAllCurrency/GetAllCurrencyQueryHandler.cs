using System.Net.Quic;
using Mcm.Catalog.Application.Features.Products.Queries.GetAllProduct;
using Mcm.Catalog.Application.Interfaces;
using Mcm.Property.Domain.Enums;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Interfaces;
using MediatR;

namespace Mcm.Catalog.Application.Features.Currencies.Queries.GetAllCurrency
{
    public class GetAllCurrencyQueryHandler(ICurrencyRepository currencyRepository)
        : IRequestHandler<GetAllCurrencyQuery, ApiResponse<GetAllCurrencyResponse>>
    {
        private readonly ICurrencyRepository _currencyRepository = currencyRepository;

        public async Task<ApiResponse<GetAllCurrencyResponse>> Handle(GetAllCurrencyQuery query, CancellationToken cancellationToken)
        {
            var currencies = await _currencyRepository.GetAllAsync(
                predicate: c => string.IsNullOrEmpty(query.Request.SearchName)
                    || c.Name.ToLower().Contains(query.Request.SearchName.ToLower()), 
                orderBy: c => c.OrderBy(c => c.Name),
                pageQuery: new PageQuery(query.Request.Page, query.Request.Limit), 
                ct: cancellationToken);

            
            var data = currencies.Select(res => new CurrencyResponse
            {
                Id = res.Id,
                Name = res.Name,
                Symbol = res.Symbol
            }).ToList();

            return new ApiResponse<GetAllCurrencyResponse>
            {
                Success = true,
                Message = "Currencies retrieved successfully",
                Code = 200,
                Data = new GetAllCurrencyResponse(data),
                Meta = new Meta
                {
                    Page = query.Request.Page,
                    Limit = query.Request.Limit,
                    Total = await _currencyRepository.CountAsync()
                }
            };
        }
    }
}