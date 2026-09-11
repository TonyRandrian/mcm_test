using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Interactions.Domain.Enums;
using Mcm.Interactions.Domain.ValueObjects;
using Mcm.Interactions.Infrastructure.Database;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Interactions.Infrastructure.Repositories
{
    public class InteractionRepository(InteractionDbContext context)
        : GenericRepository<Interaction>(context), IInteractionRepository
    {
        public async Task<bool> IsValidDate(Guid createdBy, DateTime startDate, DateTime endDate)
        {
            var interactions = await _dbSet.AsNoTracking()
            .Where(i => i.CreatedBy == createdBy)
            .ToListAsync();

            return ! interactions.Any(i => i.Date.StartDate < endDate && i.Date.EndDate > startDate);
        }

        public async Task<bool> IsValidParticipants(DateTime startDate, DateTime endDate, List<Guid> contactIds, List<Guid> memberIds)
        {
            var interactions = await _dbSet.AsNoTracking()
                .Where(i => i.Date.StartDate < endDate && i.Date.EndDate > startDate)
                .ToListAsync();
            return ! interactions
                    .Any(i => i.InteractionContacts.Any(ic => contactIds.Contains(ic.ContactId))
                        || i.InteractionMembers.Any(ic => memberIds.Contains(ic.TeamMemberId)));
        }
    }
}