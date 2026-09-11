using System.Linq.Expressions;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Shared.Infrastructure.Repositories
{
    public class GenericRepository<T>(DbContext context)
        : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? predicate = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, 
            Func<IQueryable<T>, IQueryable<T>>? selector = null, 
            PageQuery? pageQuery = null, CancellationToken ct = default)
        {
            IQueryable<T> query = _dbSet;

            if (predicate is not null) query = query.Where(predicate);
            if (orderBy is not null) query = orderBy(query);
            if (pageQuery is not null)
                query = query
                    .Skip((pageQuery.Page - 1) * pageQuery.Limit)
                    .Take(pageQuery.Limit);
            if (selector is not null)
                return await selector(query).ToListAsync(ct);
            return (IEnumerable<T>)await query.ToListAsync(ct);
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
            => await _dbSet.FirstOrDefaultAsync(e => e.Id == id);

        public async Task AddAsync(T entity)
            => await _dbSet.AddAsync(entity);

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            // _dbSet.Attach(entity);
            // _context.Entry(entity).State = EntityState.Modified;
        }

        public void HardDelete(T entity) => _dbSet.Remove(entity);

        public Task<T?> Validate(Expression<Func<T, bool>> predicate)
            => _dbSet.IgnoreQueryFilters(["SoftDelete"])
                .Where(predicate)
                .FirstOrDefaultAsync();


        public async Task<int> CountAsync()
            => await _dbSet.CountAsync();

    }
}