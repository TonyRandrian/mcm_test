using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using MediatR;

namespace Mcm.Interactions.Application.Features.Interactions.Commands.MarkAsDone
{
    public class MarkAsDoneCommandHandler(
        IInteractionRepository interactionRepository,
        IInteractionUow uow)
        : IRequestHandler<MarkAsDoneCommand, ApiResponse<MarkAsDoneResponse>>
    {
        private readonly IInteractionRepository _interactionRepository = interactionRepository;
        private readonly IInteractionUow _uow = uow;

        public async Task<ApiResponse<MarkAsDoneResponse>> Handle(MarkAsDoneCommand request, CancellationToken cancellationToken)
        {
            var interaction = await _interactionRepository.GetByIdAsync(request.InteractionId)
                ?? throw NotFoundException.NotFoundById(nameof(Interaction), request.InteractionId);

            interaction.MarkAsDone();
            await _uow.SaveChangesAsync(cancellationToken);

            return new ApiResponse<MarkAsDoneResponse>
            {
                Code = 200,
                Data = new MarkAsDoneResponse{ InteractionId = interaction.Id },
                Message = "Interaction Mark's Done successfully",
                Success = true
            };
        }
    }
}