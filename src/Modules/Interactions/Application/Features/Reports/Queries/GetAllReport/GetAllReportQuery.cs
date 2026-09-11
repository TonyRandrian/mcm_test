using Mcm.Interactions.Application.Features.InteractionTypes.Queries.GetAllInteractionType;
using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Interactions.Application.Features.Reports.Queries.GetAllReport
{
    public record GetAllReportQuery(GetAllReportRequest Request)
        : IRequest<ApiResponse<List<GetAllReportResponse>>>;
}