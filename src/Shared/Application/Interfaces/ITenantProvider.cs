namespace Mcm.Shared.Application.Interfaces
{
    public interface ITenantProvider
    {
        Guid? GetTenantId();
    }
}