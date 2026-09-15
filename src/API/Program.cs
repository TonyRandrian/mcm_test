using System.Text;
using API.Extensions;
using API.Middleware;
using Mcm.Authorizations.Application.Extensions;
using Mcm.Authorizations.Application.Features.History.Hubs;
using Mcm.Authorizations.Infrastructure.Database;
using Mcm.Authorizations.Infrastructure.Extensions;
using Mcm.Authorizations.Presentation;
using Mcm.Catalog.Application.Extensions;
using Mcm.Catalog.Infrastructure.Database;
using Mcm.Catalog.Infrastructure.Extensions;
using Mcm.Catalog.Presentation;
using Mcm.Company.Application.Extensions;
using Mcm.Company.Application.Features.Dashboard;
using Mcm.Company.Infrastructure.Database;
using Mcm.Company.Infrastructure.Extensions;
using Mcm.Company.Presentation;
using Mcm.Contacts.Application.Extensions;
using Mcm.Contacts.Infrastructure.Database;
using Mcm.Contacts.Infrastructure.Extensions;
using Mcm.Contacts.Presentation;
using Mcm.Interactions.Application.Extensions;
using Mcm.Interactions.Application.Features.Notifications;
using Mcm.Interactions.Infrastructure.Database;
using Mcm.Interactions.Infrastructure.Extensions;
using Mcm.Interactions.Presentation;
using Mcm.Property.Application.Extensions;
using Mcm.Property.Infrastructure.Database;
using Mcm.Property.Infrastructure.Extensions;
using Mcm.Property.Presentation;
using Mcm.Shared.Application.Extensions;
using Mcm.Shared.Domain.Extensions;
using Mcm.Shared.Infrastructure.Database;
using Mcm.Shared.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// DOCKER SECRETS — lecture avant tout usage de Configuration
// ============================================================
var secretsPath = "/run/secrets";
if (Directory.Exists(secretsPath))
{
    // Connection string — prod en priorité, sinon test
    var dbProdSecret = Path.Combine(secretsPath, "mcm_db_prod");
    var dbTestSecret = Path.Combine(secretsPath, "mcm_db_test");

    if (File.Exists(dbProdSecret))
        builder.Configuration["ConnectionStrings:DefaultConnection"] =
            File.ReadAllText(dbProdSecret).Trim();
    else if (File.Exists(dbTestSecret))
        builder.Configuration["ConnectionStrings:DefaultConnection"] =
            File.ReadAllText(dbTestSecret).Trim();

    // JWT Secret
    var jwtSecretFile = Path.Combine(secretsPath, "mcm_jwt_secret");
    if (File.Exists(jwtSecretFile))
        builder.Configuration["JwtSettings:SecretKey"] =
            File.ReadAllText(jwtSecretFile).Trim();

    // SMTP Password
    var smtpSecretFile = Path.Combine(secretsPath, "mcm_smtp_password");
    if (File.Exists(smtpSecretFile))
        builder.Configuration["SmtpSettings:Password"] =
            File.ReadAllText(smtpSecretFile).Trim();
}
// ============================================================

builder.Services.AddHealthChecks();
builder.Services.AddSharedApplication(builder.Configuration);
builder.Services.AddSharedInfrastructure(builder.Configuration);

builder.Services.AddPropertyApplication();
builder.Services.AddPropertyInfrastructure(builder.Configuration);

builder.Services.AddCompanyApplication();
builder.Services.AddCompanyInfrastructure(builder.Configuration);

builder.Services.AddAuthorizationApplication();
builder.Services.AddAuthorizationInfrastructure(builder.Configuration);

builder.Services.AddCatalogApplication();
builder.Services.AddCatalogInfrastructure(builder.Configuration);

builder.Services.AddContactApplication();
builder.Services.AddContactInfrastructure(builder.Configuration);

builder.Services.AddInteractionApplication();
builder.Services.AddInteractionInfrastructure(builder.Configuration);

builder.Services.AddDomainEventHandlers();

var jwtKey = builder.Configuration["JwtSettings:SecretKey"];
if (jwtKey is null || jwtKey.Length < 32)
    throw new InvalidOperationException("JwtSettings:SecretKey invalid");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer           = true,
            ValidIssuer              = builder.Configuration["JwtSettings:Issuer"],
            ValidateAudience         = true,
            ValidAudience            = builder.Configuration["JwtSettings:Audience"],
            ValidateLifetime         = true,
            ClockSkew                = TimeSpan.Zero
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hub"))
                {
                    context.Token = accessToken;
                }
                else if (string.IsNullOrEmpty(accessToken))
                {
                    var authRequest = context.Request.Headers["Authorization"].FirstOrDefault();
                    if (!string.IsNullOrEmpty(authRequest) && authRequest.StartsWith("Bearer "))
                    {
                        context.Token = authRequest.Substring("Bearer ".Length).Trim();
                    }
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new DateTimeExtension()));

builder.Services.AddPropertyPresentation();
builder.Services.AddCompanyPresentation();
builder.Services.AddAuthorizationPresentation();
builder.Services.AddCatalogPresentation();
builder.Services.AddContactPresentation();
builder.Services.AddInteractionPresentation();

builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MCM API",
        Version = "v1",
        Description = "API CRM premiere version"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Entrer token.",
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

var hosts = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
    ?? ["http://localhost:3000"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowHost", policy =>
    {
        policy.WithOrigins(hosts)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

await PostgresCreateExtension.EnsureDatabaseExistsAsync(connectionString);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var dbContexts = new DbContext[]
    {
        services.GetRequiredService<SharedDbContext>(),
        services.GetRequiredService<PropertyDbContext>(),
        services.GetRequiredService<CompanyDbContext>(),
        services.GetRequiredService<AuthorizationDbContext>(),
        services.GetRequiredService<CatalogDbContext>(),
        services.GetRequiredService<ContactDbContext>(),
        services.GetRequiredService<InteractionDbContext>()
    };

    foreach (var db in dbContexts)
    {
        try
        {
            var pending = await db.Database.GetPendingMigrationsAsync();

            if (pending.Any())
            {
                await db.Database.MigrateAsync();
            }
        }
        catch (PostgresException ex) when (ex.SqlState == "42P07")
        {
            Console.WriteLine(
                $"Migration skipped for {db.GetType().Name}: table already exists.");
        }
    }

    var catContext = scope.ServiceProvider.GetRequiredService<PropertyDbContext>();
    await SystemCategorySeeder.SystemDataAsync(catContext);

    var companyContext = scope.ServiceProvider.GetRequiredService<CompanyDbContext>();
    await SystemActivitySectorSeeder.SystemDataAsync(companyContext);

    var catalogContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    await SystemCatalogSeeder.SystemDataAsync(catalogContext);
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "MCM API v1");
    options.RoutePrefix = "swagger";
    options.DisplayRequestDuration();
    options.DefaultModelExpandDepth(-1);
    options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    options.ConfigObject.PersistAuthorization = true;
});

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("AllowHost");

var directoryPath = Path.Combine(
    builder.Environment.ContentRootPath,
    builder.Configuration["FileConfiguration:image:storage"] ?? "assets");

if (!Directory.Exists(directoryPath))
{
    Directory.CreateDirectory(directoryPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(directoryPath),
    RequestPath = "/assets"
});
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");
app.MapHub<InteractionHub>("/hub/notifications/interactions").RequireAuthorization();
app.MapHub<CompanyHub>("/hub/notifications/company");
app.MapHub<ActivityLogHub>("/hub/notifications/history");

app.Run();
