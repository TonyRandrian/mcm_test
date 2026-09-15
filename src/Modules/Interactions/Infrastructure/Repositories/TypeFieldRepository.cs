using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Interactions.Infrastructure.Database;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Interactions.Infrastructure.Repositories
{
    public class TypeFieldRepository(InteractionDbContext context)
        : GenericRepository<TypeField>(context), ITypeFieldRepository
    {
        public override Task<TypeField?> GetByIdAsync(Guid id)
        {
            IQueryable<TypeField> query = _dbSet
                .Include(tf => tf.InteractionType);
            return query.FirstOrDefaultAsync(tf => tf.Id == id);
        }
    }
}