using System.Linq.Expressions;
using Mcm.Interactions.Domain.Entities;
using Mcm.Interactions.Domain.ValueObjects;
using Mcm.Shared.Application.Interfaces;

namespace Mcm.Interactions.Application.Interfaces
{
    public interface IInteractionRepository
        : IGenericRepository<Interaction>
    {
        Task<IEnumerable<Interaction>> GetInteractionsAsync(Expression<Func<Interaction, bool>>? predicate = null);
        Task<bool> IsValidDate(Guid createdBy, DateTime startDate, DateTime endDate);
        Task<bool> IsValidParticipants(DateTime startDate, DateTime endDate, List<Guid> contactIds, List<Guid> memberIds);
        Task MarkAsDoneManyAsync(List<Guid> InteractionIds);
        Task<List<Guid>> GetMemberIdsAsync(Guid interactionId);
    }
}