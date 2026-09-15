using Mcm.Shared.Domain.Enums;

namespace Mcm.Shared.Application.Extensions
{
    public static class AdminPermission
    {
        public static List<(PermModule module, PermAction action)> Data =>
        [
            (PermModule.Category, PermAction.Create),
            (PermModule.Category, PermAction.Read),
            (PermModule.Category, PermAction.Update),
            (PermModule.Category, PermAction.Delete),

            (PermModule.Company, PermAction.Create),
            (PermModule.Company, PermAction.Read),
            (PermModule.Company, PermAction.Update),
            (PermModule.Company, PermAction.Delete),
            
            (PermModule.Contact, PermAction.Create),
            (PermModule.Contact, PermAction.Read),
            (PermModule.Contact, PermAction.Update),
            (PermModule.Contact, PermAction.Delete),

            (PermModule.Interaction, PermAction.Create),
            (PermModule.Interaction, PermAction.Read),
            (PermModule.Interaction, PermAction.Update),
            (PermModule.Interaction, PermAction.Delete),

            (PermModule.InteractionType, PermAction.Create),
            (PermModule.InteractionType, PermAction.Read),
            (PermModule.InteractionType, PermAction.Update),
            (PermModule.InteractionType, PermAction.Delete),

            (PermModule.Product, PermAction.Create),
            (PermModule.Product, PermAction.Read),
            (PermModule.Product, PermAction.Update),
            (PermModule.Product, PermAction.Delete),

            (PermModule.ProductCategory, PermAction.Create),
            (PermModule.ProductCategory, PermAction.Read),
            (PermModule.ProductCategory, PermAction.Update),
            (PermModule.ProductCategory, PermAction.Delete),

            (PermModule.Report, PermAction.Create),
            (PermModule.Report, PermAction.Read),

            (PermModule.Role, PermAction.Create),
            (PermModule.Role, PermAction.Read),
            (PermModule.Role, PermAction.Update),
            (PermModule.Role, PermAction.Delete),

            (PermModule.Service, PermAction.Create),
            (PermModule.Service, PermAction.Read),
            (PermModule.Service, PermAction.Update),
            (PermModule.Service, PermAction.Delete),

            (PermModule.ServiceCategory, PermAction.Create),
            (PermModule.ServiceCategory, PermAction.Read),
            (PermModule.ServiceCategory, PermAction.Update),
            (PermModule.ServiceCategory, PermAction.Delete),

            (PermModule.TeamMember, PermAction.Create),
            (PermModule.TeamMember, PermAction.Read),
            (PermModule.TeamMember, PermAction.Update),
            (PermModule.TeamMember, PermAction.Delete),

            (PermModule.TypeContact, PermAction.Create),
            (PermModule.TypeContact, PermAction.Read),
            (PermModule.TypeContact, PermAction.Update),
            (PermModule.TypeContact, PermAction.Delete),

        ];
    }
}