using Mcm.Property.Domain.Entities;
using Mcm.Property.Domain.Enums;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Interfaces;

namespace Mcm.Property.Application.Interfaces
{
    public interface ICategoryRepository
        : IGenericRepository<Category> 
    {
    }
}