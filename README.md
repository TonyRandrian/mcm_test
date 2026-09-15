# MCM Backend — Marketing and Community Management

API backend du CRM **MCM**, en **.NET 10**, architecture modulaire **DDD/CQRS**, base de données **PostgreSQL**.

## Stack technique

- .NET 10 / ASP.NET Core Web API
- Entity Framework Core 10
- PostgreSQL (Npgsql)
- Authentification JWT Bearer
- SignalR (notifications temps réel)
- Swagger / OpenAPI

## Prérequis

- [.NET SDK 10.0](https://dotnet.microsoft.com/download)
- PostgreSQL en local (ou via Docker) — port par défaut `5432`
- (Optionnel) Docker, si vous préférez lancer via conteneur

Il n'y a pas de fichier `.sln` à la racine : `dotnet restore`/`dotnet build` se fait directement sur le projet API, qui référence tous les modules.

```bash
dotnet build src/API
```

## 2. Configuration

La configuration se trouve dans `src/API/appsettings.json` et `src/API/appsettings.Development.json`.

### Base de données

Dans `appsettings.Development.json` :

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=mcm_test2;Username=postgres;Password=postgres"
}
```

Adaptez `Host`/`Port`/`Username`/`Password`/`Database` à votre installation PostgreSQL locale. **La base n'a pas besoin d'exister au préalable** : au démarrage, l'API crée la base si nécessaire (`PostgresCreateExtension.EnsureDatabaseExistsAsync`) puis applique automatiquement toutes les migrations EF Core en attente pour chacun des 7 `DbContext` (Shared, Property, Company, Authorization, Catalog, Contact, Interaction), avant de lancer les seeders de données système (catégories, secteurs d'activité, catalogue).

### JWT

```json
"JwtSettings": {
  "SecretKey": "...",
  "Issuer": "mcm_api",
  "Audience": "mcm_api",
  "ExpiryMinutes": 60
}
```

La clé doit faire **au moins 32 caractères**, sinon l'API refuse de démarrer (`InvalidOperationException`).

### CORS

```json
"Cors": {
  "AllowedOrigins": ["http://localhost:3000", "http://localhost:5282"]
}
```

Ajoutez ici l'URL de votre frontend si elle diffère.

### SMTP (envoi d'e-mails)

Section `SmtpSettings` dans `appsettings.Development.json`.

> ⚠️ **Attention sécurité** : `appsettings.Development.json` contient actuellement en clair une clé JWT et un mot de passe SMTP. Si ce fichier est versionné dans git, ces secrets sont considérés comme compromis. Il est recommandé de les sortir du fichier et de passer par `dotnet user-secrets` en local, et par des variables d'environnement / secrets Docker en production (le `Program.cs` sait déjà lire des secrets depuis `/run/secrets/mcm_db_prod`, `mcm_db_test`, `mcm_jwt_secret`, `mcm_smtp_password`).

Pour utiliser les user-secrets en local à la place du fichier :

```bash
cd src/API
dotnet user-secrets init
dotnet user-secrets set "JwtSettings:SecretKey" "votre-cle-secrete-32-caracteres-min"
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=mcm_test2;Username=postgres;Password=postgres"
dotnet user-secrets set "SmtpSettings:Password" "votre-mot-de-passe-app"
```

## 3. Lancer PostgreSQL (si vous n'avez pas déjà une instance)

```bash
docker run --name mcm-postgres -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -p 5432:5432 -d postgres:16
```

## 4. Lancer l'API

```bash
dotnet run --project src/API
```

Par défaut (profil `http` de `launchSettings.json`) l'API écoute sur :

- `http://localhost:5282`

Au premier lancement, la base est créée, les migrations sont appliquées et les données système sont injectées automatiquement — aucune commande manuelle n'est nécessaire.

### Accès une fois lancé

- Swagger UI : `http://localhost:5282/swagger`
- Health check : `http://localhost:5282/health`

Toutes les routes API sont protégées par défaut (`FallbackPolicy` = utilisateur authentifié) : il faut un token JWT valide, à passer via le bouton **Authorize** de Swagger.

## 5. Lancer avec Docker

Un `Dockerfile` est fourni à la racine :

```bash
docker build -t mcm-backend .
docker run -p 3000:3000 \
  -e ConnectionStrings__DefaultConnection="Host=host.docker.internal;Port=5432;Database=mcm_test2;Username=postgres;Password=postgres" \
  -e JwtSettings__SecretKey="votre-cle-secrete-32-caracteres-min" \
  mcm-backend
```

Le conteneur écoute sur le port **3000** (`ASPNETCORE_HTTP_PORTS=3000`) et tourne en environnement `Production`. En production, pensez à fournir la chaîne de connexion, la clé JWT et le mot de passe SMTP soit via variables d'environnement, soit via Docker secrets montés dans `/run/secrets/` (`mcm_db_prod`, `mcm_jwt_secret`, `mcm_smtp_password`), déjà pris en charge par `Program.cs`.

## Structure du projet

```
src/
├── API/                     # Point d'entrée, Program.cs, config, middlewares
├── Modules/
│   ├── Authorizations/      # RBAC, rôles, permissions multi-tenant
│   ├── Catalog/
│   ├── Company/
│   ├── Contacts/
│   ├── Interactions/        # Interactions, SignalR, notifications
│   └── Property/
└── Shared/
    ├── Application/         # Comportements MediatR, interfaces transverses
    ├── Domain/              # Primitives DDD partagées
    └── Infrastructure/      # DbContext partagé, repository générique, UnitOfWork
```

Chaque module suit la même découpe interne : `Application` (CQRS/MediatR), `Domain` (entités, agrégats), `Infrastructure` (EF Core, migrations), `Presentation` (controllers).