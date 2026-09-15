using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Queries.GetCompanyByToken
{
    public record GetCompanyByTokenQuery()
        : IRequest<ApiResponse<GetCompanyByTokenResponse>>;
}