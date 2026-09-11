using Mcm.Authorizations.Infrastructure.Database;
using Mcm.Shared.Application.Modules;
using Mcm.Shared.Application.Modules.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Authorizations.Infrastructure.Repositories
{
    public class TeamMemberModule(
        AuthorizationDbContext context)
        : ITeamMemberModule
    {
        private readonly AuthorizationDbContext _context = context;
        
        public async Task<bool> Exists(Guid tmId)
        {
            return await _context.TeamMembers
                .AsNoTracking()
                .AnyAsync(
                    tm => tm.Id == tmId);
        }

        public async Task<List<IdentityDto>> GetIdentities(List<Guid> tmIds)
        {
            return await _context.TeamMembers.AsNoTracking()
                .Where(tm => tmIds.Contains(tm.Id))
                .Select(tm => new IdentityDto(
                    tm.Id, 
                    tm.Identity.LastName, 
                    tm.Identity.FirstName, 
                    (tm.Image != null)? tm.Image.Url : string.Empty,
                    tm.Identity.Email))
                .ToListAsync();
        }
    }
}