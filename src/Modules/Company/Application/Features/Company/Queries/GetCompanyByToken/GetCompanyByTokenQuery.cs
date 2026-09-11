using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Queries.GetCompanyByToken
{
    public record GetCompanyByTokenQuery(GetCompanyByTokenRequest Request)
        : IRequest<ApiResponse<GetCompanyByTokenResponse>>;
}