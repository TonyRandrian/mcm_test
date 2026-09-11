using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Interactions.Application.Features.InteractionTypes.Queries.GetAllInteractionType
{
    public class GetAllInteractionTypeResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string LabelColor { get; set; } = string.Empty;
    }
}