namespace Mcm.Interactions.Application.Features.Interactions.Commands.MarkAsDoneMany
{
    public record MarkAsDoneManyRequest
    (
        List<Guid> InteractionIds
    );
}