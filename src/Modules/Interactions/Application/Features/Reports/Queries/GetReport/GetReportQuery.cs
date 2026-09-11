using Mcm.Interactions.Application.Features.TypeFields.Queries.GetTypeField;
using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Interactions.Application.Features.Reports.Queries.GetReport
{
    public record GetReportQuery(GetReportRequest Request)
        : IRequest<ApiResponse<GetReportResponse>>;
}