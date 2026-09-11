using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Queries.GetCompanyById
{
    public record GetCompanyByIdQuery(GetCompanyByIdRequest Request)
        : IRequest<ApiResponse<GetCompanyByIdResponse>>;
}