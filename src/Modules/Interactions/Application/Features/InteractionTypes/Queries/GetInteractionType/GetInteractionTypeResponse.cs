using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Interactions.Application.Features.InteractionTypes.Queries.GetInteractionType
{
    public class GetInteractionTypeResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string LabelColor { get; set; } = string.Empty; 
        public string? Description { get; set; }
        public List<GetInteractionTypeResponse>? SubTypes { get; set; } = [];
    }
}