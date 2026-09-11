using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Mcm.Interactions.Application.Features.Reports.Commands.CreateReport
{
    public class CreateReportCommand
        : IRequest<ApiResponse<CreateReportResponse>>
    {
        public Guid InteractionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ActionPlan { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public List<Guid> Contacts { get; set; } = [];
        public List<Guid> TeamMembers { get; set; } = [];
        public List<IFormFile> Attachments { get; set; } = [];
    }
}