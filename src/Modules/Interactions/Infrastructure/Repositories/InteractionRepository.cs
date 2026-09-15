using System.Linq.Expressions;
using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Interactions.Infrastructure.Database;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Interactions.Infrastructure.Repositories
{
    public class InteractionRepository(InteractionDbContext context)
        : GenericRepository<Interaction>(context), IInteractionRepository
    {
        public override Task<Interaction?> GetByIdAsync(Guid id)
        {
            IQueryable<Interaction> query = _dbSet
                .Include(i => i.Type)
                .Include(i => i.InteractionMembers)
                .Include(i => i.InteractionContacts)
                .Include(i => i.Report);
            return query.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Interaction>> GetInteractionsAsync(
            Expression<Func<Interaction, bool>>? predicate = null)
        {
            var query = _dbSet
                .IgnoreQueryFilters(["MultiTenant"]);
            if (predicate is not null) query = query.Where(predicate);
            return await query.ToListAsync();
        }

        public async Task<List<Guid>> GetMemberIdsAsync(Guid interactionId)
        {
            return await _dbSet
                .IgnoreQueryFilters(["MultiTenant"])
                .Include(i => i.InteractionMembers)
                .Where(i => i.Id == interactionId)
                .SelectMany(i => i.InteractionMembers)
                .Select(im => im.TeamMemberId)
                .ToListAsync();
        }

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

        public async Task MarkAsDoneManyAsync(List<Guid> InteractionIds)
        {
            var interactions = await _dbSet
                .Where(i => InteractionIds.Contains(i.Id))
                .ToListAsync();

            interactions.ForEach(i => i.MarkAsDone());
        }
    }
}