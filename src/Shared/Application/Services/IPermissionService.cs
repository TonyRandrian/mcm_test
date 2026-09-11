namespace Mcm.Company.Application.Interfaces
{
    public interface IPermissionService
    {
        Task<HashSet<string>> GetPermissions(Guid teamMemberId);
    }
}