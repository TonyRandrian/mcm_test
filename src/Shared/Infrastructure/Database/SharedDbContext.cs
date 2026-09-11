using Mcm.Shared.Application.Exceptions;
using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Interfaces;
using Mcm.Shared.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Shared.Infrastructure.Database
{
    public class SharedDbContext(DbContextOptions<SharedDbContext> options)
        : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Shared");

            base.OnModelCreating(modelBuilder);
        }
    }
}