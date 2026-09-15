using System.Linq.Expressions;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Domain.Primitives;

namespace Mcm.Shared.Application.Interfaces
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, object?>>[]? includes = null,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IQueryable<T>>? selector = null,
            PageQuery? pageQuery = null,
            CancellationToken ct = default);
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> Validate(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task AddManyAsync(List<T> entities);
        void Update(T entity);
        void HardDelete(T entity);
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);

    }
}