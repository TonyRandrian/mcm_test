using Mcm.Authorizations.Domain.Entities;
using Mcm.Shared.Application.Interfaces;

namespace Mcm.Authorizations.Application.Interfaces
{
    public interface IRefreshTokenRepository
        : IGenericRepository<RefreshToken>
    {
        Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken);
    }
}