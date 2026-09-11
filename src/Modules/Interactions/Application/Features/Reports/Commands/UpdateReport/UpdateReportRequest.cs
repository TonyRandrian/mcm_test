using Mcm.Shared.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Mcm.Interactions.Application.Features.Reports.Commands.UpdateReport
{
    public record UpdateReportRequest
    (
        string Title,
        string LabelColor,
        string? Description
    );
}