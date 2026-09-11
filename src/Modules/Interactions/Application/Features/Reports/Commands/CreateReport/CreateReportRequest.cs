using Mcm.Shared.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mcm.Interactions.Application.Features.Reports.Commands.CreateReport
{
    public record CreateReportRequest
    (
        string Name,
        string Description,
        string ActionPlan,
        CreateReport_Date Date,
        CreateReport_Present Present,
        List<IFormFile> Attachments
    );

    public record CreateReport_Date(string StartDate, string EndDate);
    public record CreateReport_Present(List<Guid> Contacts, List<Guid> TeamMembers);
}