using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.ValueObjects;
using MediatR;

namespace Mcm.Interactions.Application.Features.InteractionTypes.Commands.UpdateInteractionType
{
    public class UpdateInteractionTypeCommandHandler(
        IInteractionTypeRepository typeRepository,
        IInteractionUow uow)
        : IRequestHandler<UpdateInteractionTypeCommand, ApiResponse<UpdateInteractionTypeResponse>>
    {
        private readonly IInteractionTypeRepository _typeRepository = typeRepository;
        private readonly IInteractionUow _uow = uow;

        public async Task<ApiResponse<UpdateInteractionTypeResponse>> Handle(UpdateInteractionTypeCommand command, CancellationToken ct)
        {
            var type = await _typeRepository.GetByIdAsync(command.Id)
                ?? throw NotFoundException.NotFoundById(nameof(InteractionType), command.Id); 

            type.Update( 
                command.Title,
                command.LabelColor,
                command.Description);

            _typeRepository.Update(type);
            await _uow.SaveChangesAsync(ct);

            return new ApiResponse<UpdateInteractionTypeResponse>
            {
                Success = true,
                Message = "Update InteractionType successfulling",
                Code = 200,
                Data = new UpdateInteractionTypeResponse{InteractionTypeId = type.Id}
            };
        }
    }
}