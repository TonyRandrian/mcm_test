using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Interactions.Infrastructure.Database;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Interactions.Infrastructure.Repositories
{
    public class InteractionTypeRepository(InteractionDbContext context)
        : GenericRepository<InteractionType>(context), IInteractionTypeRepository
    {
        public async Task<List<Guid>> GetAncestors(Guid typeId)
        {
            List<InteractionType> ancestors = [];
            var currentType = await _dbSet.FirstOrDefaultAsync(t => t.Id == typeId)
                ?? throw new ArgumentException("Invalid InteractionTypeId");
            if (currentType is not null)
                ancestors.Add(currentType);
            while (currentType?.ParentId is not null)
            {
                currentType = await _dbSet.FirstOrDefaultAsync(t => t.Id == currentType.ParentId);
                if (currentType is not null)
                    ancestors.Add(currentType);
            }
            return ancestors.Select(t => t.Id).ToList();
        }
    }
}