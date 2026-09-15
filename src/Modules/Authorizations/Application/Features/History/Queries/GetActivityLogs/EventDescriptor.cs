using System.Text.Json;

namespace Mcm.Authorizations.Application.Features.History.Queries.GetActivityLogs
{
    public static class EventDescriptor
    {
        public static readonly Dictionary<string, (string Category, string Label)> Config = new()
        {
            ["RoleCreatedEvent"] = ("creation", "Rôle créée"),
            ["RoleUpdatedEvent"] = ("modification", "Rôle modifiée"),
            ["RoleDeletedEvent"] = ("suppression", "Rôle supprimée"),
            ["TeamMemberInvitedEvent"] = ("invitation", "Membre invité"),
            ["ProductCreatedEvent"] = ("creation", "Produit créé"),
            ["ProductUpdatedEvent"] = ("modification", "Produit modifié"),
            ["ProductDeletedEvent"] = ("suppression", "Produit supprimé"),
            ["ProductCategoryCreatedEvent"] = ("creation", "Categorie de Produit créée"),
            ["ProductCategoryUpdatedEvent"] = ("modification", "Categorie de Produit modifiée"),
            ["ProductCategoryDeletedEvent"] = ("suppression", "Categorie de Produit supprimée"),
            ["ServiceCreatedEvent"] = ("creation", "Service créé"),
            ["ServiceUpdatedEvent"] = ("modification", "Service modifié"),
            ["ServiceDeletedEvent"] = ("suppression", "Service supprimé"),
            ["ServiceCategoryCreatedEvent"] = ("creation", "Categorie de Service créée"),
            ["ServiceCategoryUpdatedEvent"] = ("modification", "Categorie de Service modifiée"),
            ["ServiceCategoryDeletedEvent"] = ("suppression", "Categorie de Service supprimée"),
            ["CompanyContactCreatedEvent"] = ("creation", "Entreprise-Contact créée"),
            ["CompanyContactUpdatedEvent"] = ("modification", "Entreprise-Contact modifiée"),
            ["CompanyContactDeletedEvent"] = ("suppression", "Entreprise-Contact supprimée"),
            ["SubsidiaryCreatedEvent"] = ("creation", "Sous-Entreprise créé"),
            ["SubsidiaryUpdatedEvent"] = ("modification", "Sous-Entreprise modifié"),
            ["SubsidiaryDeletedEvent"] = ("suppression", "Sous-Entreprise supprimé"),
            ["TypeContactCreatedEvent"] = ("creation", "Type de Contact créé"),
            ["TypeContactUpdatedEvent"] = ("modification", "Type de Contact modifié"),
            ["TypeContactDeletedEvent"] = ("suppression", "Type de Contact supprimé"),
            // ["CompanyCreatedEvent"] = ("creation", "Company créée"),
            ["CompanyUpdatedEvent"] = ("modification", "Entreprise modifiée"),
            ["CompanyDeletedEvent"] = ("suppression", "Entreprise supprimée"),
            ["ContactCreatedEvent"] = ("creation", "Contact créée"),
            ["ContactUpdatedEvent"] = ("modification", "Contact modifiée"),
            ["ContactDeletedEvent"] = ("suppression", "Contact supprimée"),
            ["InteractionCreatedEvent"] = ("creation", "Activité créée"),
            ["InteractionUpdatedEvent"] = ("modification", "Activité modifiée"),
            ["InteractionDeletedEvent"] = ("suppression", "Activité supprimée"),
            ["InteractionTypeCreatedEvent"] = ("creation", "Type d'Activité créé"),
            ["InteractionTypeUpdatedEvent"] = ("modification", "Type d'Activité modifié"),
            ["InteractionTypeDeletedEvent"] = ("suppression", "Type d'Activité supprimé"),
            ["TypeFieldCreatedEvent"] = ("creation", "Champ Type d'Activité créé"),
            ["TypeFieldUpdatedEvent"] = ("modification", "Champ Type d'Activité modifié"),
            ["TypeFieldDeletedEvent"] = ("suppression", "Champ Type d'Activité supprimé"),
            ["ReportCreatedEvent"] = ("creation", "Compte Rendu créé"),
            ["ReportDeletedEvent"] = ("suppression", "Compte Rendu supprimé"),
            ["CategoryCreatedEvent"] = ("creation", "Categorie de Données créée"),
            ["CategoryUpdatedEvent"] = ("modification", "Categorie de Données modifiée"),
            ["CategoryDeletedEvent"] = ("suppression", "Category de Données supprimée"),
            ["PropertyCreatedEvent"] = ("creation", "Propriété de Données créée"),
            ["PropertyUpdatedEvent"] = ("modification", "Propriété de Données modifiée"),
            ["PropertyDeletedEvent"] = ("suppression", "Propriété de Données supprimée"),
        };

        public static (string Category, string Label) Describe(string eventType)
        {
            return Config.TryGetValue(eventType, out var value)
                ? value
                : ("Autre", eventType);
        }

        public static string Detail(string eventType, string payloadJson)
        {
            using var doc = JsonDocument.Parse(payloadJson);
            var root = doc.RootElement;

            string? TryGet(string prop) =>
                root.TryGetProperty(prop, out var v)
                    ? v.GetString() 
                    : null;

            return eventType switch
            {
                "RoleCreatedEvent" =>
                    $"Rôle «{TryGet("Title")}» créée",
                "RoleUpdatedEvent" => 
                    $"Rôle «{TryGet("Title")}» modifiée",
                "RoleDeletedEvent" => 
                    $"Rôle «{TryGet("Title")}» supprimée",
                "TeamMemberInvitedEvent" =>
                    $"Membre «{TryGet("Identity.Email.Value")}» invitée",
                "ProductCreatedEvent" =>
                    $"Produit «{TryGet("Name")}» créée",
                "ProductUpdatedEvent" =>
                    $"Produit «{TryGet("Name")}» modifiée",
                "ProductDeletedEvent" =>
                    $"Produit «{TryGet("Name")}» supprimée",
                "ProductCategoryCreatedEvent" =>
                    $"Categorie de Produit «{TryGet("Name")}» créée",
                "ProductCategoryUpdatedEvent" =>
                    $"Categorie de Produit «{TryGet("Name")}» modifiée",
                "ProductCategoryDeletedEvent" =>
                    $"Categorie de Produit «{TryGet("Name")}» supprimée",
                "ServiceCreatedEvent" =>
                    $"Service «{TryGet("Name")}» créée",
                "ServiceUpdatedEvent" =>
                    $"Service «{TryGet("Name")}» modifiée",
                "ServiceDeletedEvent" =>
                    $"Service «{TryGet("Name")}» supprimée",
                "ServiceCategoryCreatedEvent" =>
                    $"Categorie de Service «{TryGet("Name")}» créée",
                "ServiceCategoryUpdatedEvent" =>
                    $"Categorie de Service «{TryGet("Name")}» modifiée",
                "ServiceCategoryDeletedEvent" =>
                    $"Categorie de Service «{TryGet("Name")}» supprimée",
                "CompanyContactCreatedEvent" =>
                    $"Entreprise-Contact «{TryGet("Name")}» créée",
                "CompanyContactUpdatedEvent" =>
                    $"Entreprise-Contact «{TryGet("Name")}» modifiée",
                "CompanyContactDeletedEvent" =>
                    $"Entreprise-Contact «{TryGet("Name")}» supprimée",
                "SubsidiaryCreatedEvent" =>
                    $"Sous-Entreprise «{TryGet("Name")}» créée",
                "SubsidiaryUpdatedEvent" =>
                    $"Sous-Entreprise «{TryGet("Name")}» modifiée",
                "SubsidiaryDeletedEvent" =>
                    $"Sous-Entreprise «{TryGet("Name")}» supprimée",
                "TypeContactCreatedEvent" =>
                    $"Type de Contact «{TryGet("Name")}» créée",
                "TypeContactUpdatedEvent" =>
                    $"Type de Contact «{TryGet("Name")}» modifiée",
                "TypeContactDeletedEvent" =>
                    $"Type de Contact «{TryGet("Name")}» supprimée",
                "CompanyUpdatedEvent" =>
                    $"Entreprise «{TryGet("Name")}» modifiée",
                "CompanyDeletedEvent" =>
                    $"Entreprise «{TryGet("Name")}» supprimée",
                "ContactCreatedEvent" =>
                    $"Contact «{TryGet("FullName")}» créée",
                "ContactUpdatedEvent" =>
                    $"Contact «{TryGet("FullName")}» modifiée",
                "ContactDeletedEvent" =>
                    $"Contact «{TryGet("FullName")}» supprimée",
                "InteractionCreatedEvent" =>
                    $"Activité «{TryGet("Title")}» créée",
                "InteractionUpdatedEvent" =>
                    $"Activité «{TryGet("Title")}» modifiée",
                "InteractionDeletedEvent" =>
                    $"Activité «{TryGet("Title")}» supprimée",
                "InteractionTypeCreatedEvent" =>
                    $"Type d'Activité «{TryGet("Title")}» créée",
                "InteractionTypeUpdatedEvent" =>
                    $"Type d'Activité «{TryGet("Title")}» modifiée",
                "InteractionTypeDeletedEvent" =>
                    $"Type d'Activité «{TryGet("Title")}» supprimée",
                "TypeFieldCreatedEvent" =>
                    $"Champ Type d'Activité «{TryGet("Name")}» créée",
                "TypeFieldUpdatedEvent" =>
                    $"Champ Type d'Activité «{TryGet("Name")}» modifiée",
                "TypeFieldDeletedEvent" =>
                    $"Champ Type d'Activité «{TryGet("Name")}» supprimée",
                "ReportCreatedEvent" =>
                    $"Compte Rendu «{TryGet("Name")}» créée",
                "ReportDeletedEvent" =>
                    $"Compte Rendu «{TryGet("Name")}» supprimée",
                "CategoryCreatedEvent" =>
                    $"Catégorie de données «{TryGet("Name")}» créée",
                "CategoryUpdatedEvent" =>
                    $"Catégorie de données «{TryGet("Name")}» modifiée",
                "CategoryDeletedEvent" =>
                    $"Catégorie de données «{TryGet("Name")}» supprimée",
                "PropertyCreatedEvent" =>
                    $"Propriété  de données «{TryGet("Name")}» créée",
                "PropertyUpdatedEvent" =>
                    $"Propriété  de données «{TryGet("Name")}» modifiée",
                "PropertyDeletedEvent" =>
                    $"Propriété  de données «{TryGet("Name")}» supprimée",
                _ => "Detail non disponible"
            };
        }
    }
}