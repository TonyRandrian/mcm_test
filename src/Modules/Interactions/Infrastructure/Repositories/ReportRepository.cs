using Mcm.Interactions.Application.Interfaces;
using Mcm.Interactions.Domain.Entities;
using Mcm.Interactions.Infrastructure.Database;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Interactions.Infrastructure.Repositories
{
    public class ReportRepository(InteractionDbContext context)
        : GenericRepository<Report>(context), IReportRepository
    {
    }
}