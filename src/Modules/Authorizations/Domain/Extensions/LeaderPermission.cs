using Mcm.Shared.Domain.Enums;

namespace Mcm.Authorizations.Domain.Extensions
{
    public static class LeaderPermission
    {
        public static List<(PermModule, PermAction)> GetValues()
        {
            return
            [
                (PermModule.Company, PermAction.Create),
                (PermModule.Company, PermAction.Read),
                (PermModule.Company, PermAction.Update),
                (PermModule.TeamMember, PermAction.Create),
                (PermModule.TeamMember, PermAction.Delete),
                (PermModule.TeamMember, PermAction.Read),
                (PermModule.Contact, PermAction.Delete),
                (PermModule.Contact, PermAction.Create),
                (PermModule.Contact, PermAction.Update),
                (PermModule.Contact, PermAction.Read),
                (PermModule.Category, PermAction.Read),
                (PermModule.Interaction, PermAction.Create),
                (PermModule.Interaction, PermAction.Update),
                (PermModule.Interaction, PermAction.Read),
                (PermModule.Interaction, PermAction.Delete),
                (PermModule.InteractionType, PermAction.Create),
                (PermModule.InteractionType, PermAction.Read),
                (PermModule.Product, PermAction.Delete),
                (PermModule.Product, PermAction.Create),
                (PermModule.Product, PermAction.Update),
                (PermModule.Product, PermAction.Read),
                (PermModule.ProductCategory, PermAction.Delete),
                (PermModule.ProductCategory, PermAction.Create),
                (PermModule.ProductCategory, PermAction.Update),
                (PermModule.ProductCategory, PermAction.Read),
                (PermModule.Service, PermAction.Delete),
                (PermModule.Service, PermAction.Create),
                (PermModule.Service, PermAction.Update),
                (PermModule.Service, PermAction.Read),
                (PermModule.ServiceCategory, PermAction.Delete),
                (PermModule.ServiceCategory, PermAction.Create),
                (PermModule.ServiceCategory, PermAction.Update),
                (PermModule.ServiceCategory, PermAction.Read),
                (PermModule.Role, PermAction.Create),
                (PermModule.Role, PermAction.Update),
                (PermModule.Report, PermAction.Create),
                (PermModule.Report, PermAction.Read)
            ];
        }
    }
}