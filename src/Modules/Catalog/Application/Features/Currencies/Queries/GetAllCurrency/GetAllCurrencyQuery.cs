using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Catalog.Application.Features.Currencies.Queries.GetAllCurrency
{
    public record GetAllCurrencyQuery(GetAllCurrencyRequest Request)
        : IRequest<ApiResponse<GetAllCurrencyResponse>>;
}