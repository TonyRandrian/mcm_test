namespace Mcm.Shared.Application.Services
{
    public interface IPermissionService
    {
        Task<HashSet<string>> GetPermissions(Guid teamMemberId, Guid companyId);
        Task<List<(string Module, HashSet<string> Action)>> GetActions(Guid teamMemberId, Guid companyId);
    }
}