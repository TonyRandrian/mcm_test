using Mcm.Property.Application.Interfaces;
using Mcm.Property.Domain.Entities;
using Mcm.Property.Domain.Enums;
using Mcm.Property.Infrastructure.Database;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Property.Infrastructure.Repositories;

public class CategoryRepository(PropertyDbContext context)
    : GenericRepository<Category>(context), ICategoryRepository
{
}