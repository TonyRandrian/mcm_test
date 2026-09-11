using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Enums;
using MediatR;

namespace Mcm.Interactions.Application.Features.Reports.Commands.CreateReport
{
    public class CreateReportCommandHandler(
        IInteractionRepository interactionRepository,
        IReportRepository reportRepository,
        IResourceService resourceService,
        IInteractionUow uow)
        : IRequestHandler<CreateReportCommand, ApiResponse<CreateReportResponse>>
    {
        private readonly IReportRepository _reportRepository = reportRepository;
        private readonly IInteractionRepository _interactionRepository = interactionRepository;
        private readonly IResourceService _resourceService = resourceService;
        private readonly IInteractionUow _uow = uow;

        public async Task<ApiResponse<CreateReportResponse>> Handle(CreateReportCommand command, CancellationToken ct)
        {
            var interaction = await _interactionRepository.GetByIdAsync(command.InteractionId)
                ?? throw NotFoundException.NotFoundById(nameof(Interaction), command.InteractionId);
            if (interaction.ReportId is not null)
                throw new BadRequestException("This Interaction already has report");

            var report = Report.Create(
                interaction.Id,
                command.Name,
                command.Description,
                command.ActionPlan,
                command.StartDate,
                command.EndDate);
            
            interaction.SetReport(report.Id);

            report.SyncContact(command.Contacts);
            report.SyncMember(command.TeamMembers);
            
            foreach (var attachment in command.Attachments)
            {
                report.AddResource(await _resourceService.SaveResource(attachment, FileType.Document | FileType.Image));
            }

            await _reportRepository.AddAsync(report);
            await _uow.SaveChangesAsync(ct);
            return new ApiResponse<CreateReportResponse>
            {
                Success = true,
                Message = "Report created succesfully",
                Code = 200,
                Data = new CreateReportResponse{ReportId = report.Id}
            };
        }
    }
}