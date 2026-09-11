using Mcm.Company.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mcm.Company.Infrastructure.Database
{
    public static class SystemActivitySectorSeeder
    {
        public static async Task SystemDataAsync(CompanyDbContext context)
        {
            if (await context.ActivitySectors.AnyAsync())
                return;
            
            var activitySectors = new List<Domain.Entities.ActivitySector>
            {
                ActivitySector.Create("Activités juridiques et comptables"),
                ActivitySector.Create("Agriculture et élevage"),
                ActivitySector.Create("Architecture, études et normes"),
                ActivitySector.Create("Artisanat d'art, audiovisuel et spectacle"),
                ActivitySector.Create("Automobile"),
                ActivitySector.Create("Bâtiment et travaux publics (BTP)"),
                ActivitySector.Create("Commerce et distribution"),
                ActivitySector.Create("Communication et marketing"),
                ActivitySector.Create("Culture et patrimoine"),
                ActivitySector.Create("Enseignement et formation"),
                ActivitySector.Create("Environnement"),
                ActivitySector.Create("Finance, banque et assurance"),
                ActivitySector.Create("Gestion administrative et ressources humaines"),
                ActivitySector.Create("Hôtellerie et restauration"),
                ActivitySector.Create("Immobilier"),
                ActivitySector.Create("Industrie - Alimentaire"),
                ActivitySector.Create("Industrie - Bois"),
                ActivitySector.Create("Industrie - Chimie"),
                ActivitySector.Create("Industrie - Métallurgie"),
                ActivitySector.Create("Industrie - Papier et imprimerie"),
                ActivitySector.Create("Industrie - Textile et mode"),
                ActivitySector.Create("Industrie - Électronique"),
                ActivitySector.Create("Industries"),
                ActivitySector.Create("Informatique et télécommunication"),
                ActivitySector.Create("Logistique et transport"),
                ActivitySector.Create("Maintenance, entretien et nettoyage"),
                ActivitySector.Create("Recherche"),
                ActivitySector.Create("Santé"),
                ActivitySector.Create("Service public, défense et sécurité"),
                ActivitySector.Create("Service à la personne"),
                ActivitySector.Create("Social"),
                ActivitySector.Create("Sport, animation et loisir"),
                ActivitySector.Create("Tourisme"),
                ActivitySector.Create("Édition"),
                ActivitySector.Create("Énergie")
            };
            context.ActivitySectors.AddRange(activitySectors);

            await context.SaveChangesAsync();
        }
    }
}