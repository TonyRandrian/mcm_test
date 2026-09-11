// using Mcm.Interactions.Application.Features.InteractionTypes.Commands.UpdateInteractionType;
// using Mcm.Interactions.Application.Interfaces;
// using Mcm.Interactions.Domain.Entities;
// using Mcm.Shared.Application.Common;
// using Mcm.Shared.Application.Exceptions;
// using Mcm.Shared.Application.Interfaces;
// using Mcm.Shared.Domain.Enums;
// using Mcm.Shared.Domain.ValueObjects;
// using MediatR;

// namespace Mcm.Interactions.Application.Features.Reports.Commands.UpdateReport
// {
//     public class UpdateTypeFieldCommandHandler(
//         ITypeFieldRepository fieldRepository,
//         IInteractionUow uow)
//         : IRequestHandler<UpdateTypeFieldCommand, ApiResponse<UpdateTypeFieldResponse>>
//     {
//         private readonly ITypeFieldRepository _fieldRepository = fieldRepository;
//         private readonly IInteractionUow _uow = uow;

//         public async Task<ApiResponse<UpdateTypeFieldResponse>> Handle(UpdateTypeFieldCommand command, CancellationToken ct)
//         {
//             var field = await _fieldRepository.GetByIdAsync(command.TypeFieldId)
//                 ?? throw NotFoundException.NotFoundById(nameof(Report), command.TypeFieldId); 

//             field.Update( 
//                 command.Name,
//                 command.Type);

//             _fieldRepository.Update(field);
//             await _uow.SaveChangesAsync(ct);

//             return new ApiResponse<UpdateTypeFieldResponse>
//             {
//                 Success = true,
//                 Message = "Update Report successfull",
//                 Code = 200,
//                 Data = new UpdateTypeFieldResponse{TypeFieldId = field.Id}
//             };
//         }
//     }
// }