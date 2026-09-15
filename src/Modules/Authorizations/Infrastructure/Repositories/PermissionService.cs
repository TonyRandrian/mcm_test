using Mcm.Authorizations.Infrastructure.Database;
using Mcm.Shared.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Authorizations.Infrastructure.Repositories
{
    public class PermissionService(
        AuthorizationDbContext context)
        : IPermissionService
    {
        private readonly AuthorizationDbContext _context = context;

        public async Task<List<(string Module, HashSet<string> Action)>> GetActions(Guid teamMemberId, Guid companyId)
        {
            var permissions = await _context.Roles
                .Include(r => r.Members)
                .Include(r => r.RoleCompanies)
                .Where(r => 
                    r.Members.Any(m => m.TeamMemberId == teamMemberId) &&
                    r.RoleCompanies.Any(m => m.CompanyId == companyId))
                .SelectMany(r => r.Authorizations)
                .GroupBy(a => a.Module)
                .Select(a => new
                {
                    Module = a.Key.ToString(),
                    Actions = a.Select(p => p.Action.ToString()).ToList()
                }).ToListAsync();
        
            return permissions
                .Select(p => (p.Module, p.Actions.ToHashSet()))
                .ToList();
        }

        public async Task<HashSet<string>> GetPermissions(Guid teamMemberId, Guid companyId)
        {
            var permissions = await _context.Roles
                .Include(r => r.Members)
                .Include(r => r.RoleCompanies)
                .Where(r => 
                    r.Members.Any(m => m.TeamMemberId == teamMemberId) &&
                    r.RoleCompanies.Any(m => m.CompanyId == companyId))
                .SelectMany(r => r.Authorizations)
                .Select(a => $"{a.Module.ToString()}:{a.Action.ToString()}")
                .ToListAsync();
        
            return [.. permissions];
        }
    }
}