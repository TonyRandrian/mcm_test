using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Company.Application.Features.Company.Queries.GetSubsidiaries
{
    public record GetSubsidiariesQuery(GetSubsidiariesRequest Request)
        : IRequest<ApiResponse<GetSubsidiariesResponse>>;
}