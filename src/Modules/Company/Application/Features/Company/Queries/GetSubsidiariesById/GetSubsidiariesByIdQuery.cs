using Mcm.Company.Application.Features.Company.Queries.GetSubsidiaries;
using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Queries.GetSubsidiariesById
{
    public record GetSubsidiariesByIdQuery(
        Guid CompanyId, int Page, int Limit)
        : IRequest<ApiResponse<GetSubsidiariesByIdResponse>>;
}