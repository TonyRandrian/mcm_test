using Mcm.Catalog.Domain.Entities;
using Mcm.Shared.Application.Common;
using Mcm.Shared.Application.Interfaces;

namespace Mcm.Catalog.Application.Interfaces
{
    public interface IServiceRepository
        : IGenericRepository<Service>
    {
    }
}