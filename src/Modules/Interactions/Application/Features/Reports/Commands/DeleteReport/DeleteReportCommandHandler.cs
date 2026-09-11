using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using MediatR;

namespace Mcm.Interactions.Application.Features.Reports.Commands.DeleteReport
{
    public class DeleteReportCommandHandler(
        IReportRepository reportRepository, 
        IInteractionUow uow)
        : IRequestHandler<DeleteReportCommand, ApiResponse<DeleteReportResponse>>
    {
        private readonly IReportRepository _reportRepository = reportRepository;
        private readonly IInteractionUow _uow = uow;

        public async Task<ApiResponse<DeleteReportResponse>> Handle(DeleteReportCommand command, CancellationToken ct)
        {
            var report = await _reportRepository.GetByIdAsync(command.Id)
                ?? throw NotFoundException.NotFoundById(nameof(Report), command.Id);
            
            if (command.Force)
                _reportRepository.HardDelete(report);
            else
            {
                report.Delete();
                _reportRepository.Update(report);
            }
            await _uow.SaveChangesAsync(ct);

            return new ApiResponse<DeleteReportResponse>{
                Success = true,
                Message = "Delete Report successfully",
                Code = 200,
                Data = new DeleteReportResponse{Unit = Unit.Value}
            };
        }
    }
}