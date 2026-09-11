namespace Mcm.Shared.Domain.Interfaces
{
    public interface ITenantScoped
    {
        Guid TenantId { get; set; }
    }
}