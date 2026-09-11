using Mcm.Interactions.Domain.Entities;
using Mcm.Shared.Application.Interfaces;

namespace Mcm.Interactions.Application.Interfaces
{
    public interface IInteractionTypeRepository
        : IGenericRepository<InteractionType>
    {
        Task<List<Guid>> GetAncestors(Guid typeId);
    }
}