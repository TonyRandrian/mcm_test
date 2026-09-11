using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Interactions.Application.Features.Reports.Commands.UpdateReport
{
    public class UpdateReportCommand
        : IRequest<ApiResponse<UpdateReportResponse>>
    {
        public Guid TypeFieldId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}