using Mcm.Shared.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Mcm.Shared.Infrastructure.Repositories
{
    public class UnitOfWork<TContext>(TContext context)
        : IUnitOfWork where TContext: DbContext
    {
        private readonly TContext _context = context;
        private IDbContextTransaction? _transaction;
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken);

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
                throw new InvalidOperationException("Transaction already started.");
            _transaction = await _context.Database.BeginTransactionAsync();
        }


        public async Task CommitAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction not started.");
            
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}