using System.Text.Json.Serialization;
using Mcm.Shared.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Interactions.Application.Features.Reports.Commands.DeleteReport
{
    public class DeleteReportCommand
        : IRequest<ApiResponse<DeleteReportResponse>>
    {
       public Guid Id { get; set; }
       public bool Force { get; set; }
    }
}