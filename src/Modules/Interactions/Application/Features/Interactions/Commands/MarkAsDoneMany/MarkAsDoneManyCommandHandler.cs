using Mcm.Interactions.Application.Features.Interactions.Commands.MarkAsDone;
using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using MediatR;

namespace Mcm.Interactions.Application.Features.Interactions.Commands.MarkAsDoneMany
{
    public class MarkAsDoneManyCommandHandler(
        IInteractionRepository interactionRepository,
        IInteractionUow uow)
        : IRequestHandler<MarkAsDoneManyCommand, ApiResponse<MarkAsDoneManyResponse>>
    {
        private readonly IInteractionRepository _interactionRepository = interactionRepository;
        private readonly IInteractionUow _uow = uow;

        public async Task<ApiResponse<MarkAsDoneManyResponse>> Handle(MarkAsDoneManyCommand request, CancellationToken cancellationToken)
        {
            await _interactionRepository.MarkAsDoneManyAsync(request.InteractionIds);

            await _uow.SaveChangesAsync(cancellationToken);

            return new ApiResponse<MarkAsDoneManyResponse>
            {
                Code = 200,
                Data = new MarkAsDoneManyResponse(),
                Message = "Interactions Mark Done successfully",
                Success = true
            };
        }
    }
}