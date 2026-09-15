using Mcm.Shared.Application.Common;
using MediatR;

namespace Mcm.Interactions.Application.Features.TypeFields.Commands.CreateMultipleTypeField
{
    public class CreateMultipleTypeFieldCommand
        : IRequest<ApiResponse<CreateMultipleTypeFieldResponse>>
    {
        public Guid InteractionTypeId { get; set; }
        public List<TypeFieldCommand> MultipleField { get; set; } = [];
    }

    public class TypeFieldCommand
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}