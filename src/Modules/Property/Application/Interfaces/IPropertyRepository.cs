using Mcm.Shared.Application.Interfaces;

namespace Mcm.Property.Application.Interfaces
{
    public interface IPropertyRepository
        : IGenericRepository<Domain.Entities.Property>
    {
    }
}