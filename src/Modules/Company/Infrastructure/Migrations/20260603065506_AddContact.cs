using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Company.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Company");

            migrationBuilder.CreateTable(
                name: "ActivitySectors",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivitySectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "company_typecontacts",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeConvertTo = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company_typecontacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_company_typecontacts_company_typecontacts_TypeConvertTo",
                        column: x => x.TypeConvertTo,
                        principalSchema: "Company",
                        principalTable: "company_typecontacts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "companies",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Acronym = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    logo_storage_type = table.Column<int>(type: "integer", nullable: false),
                    logo_url = table.Column<string>(type: "text", nullable: false),
                    logo_alternative_text = table.Column<string>(type: "text", nullable: false),
                    logo_file_type = table.Column<int>(type: "integer", nullable: false),
                    IsContact = table.Column<bool>(type: "boolean", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    TypeContactId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_companies_companies_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Company",
                        principalTable: "companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_companies_company_typecontacts_TypeContactId",
                        column: x => x.TypeContactId,
                        principalSchema: "Company",
                        principalTable: "company_typecontacts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "company_activities",
                schema: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivitySectorId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company_activities", x => new { x.CompanyId, x.ActivitySectorId });
                    table.ForeignKey(
                        name: "FK_company_activities_ActivitySectors_ActivitySectorId",
                        column: x => x.ActivitySectorId,
                        principalSchema: "Company",
                        principalTable: "ActivitySectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_company_activities_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Company",
                        principalTable: "companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyValue",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyValue_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Company",
                        principalTable: "companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_companies_ParentId",
                schema: "Company",
                table: "companies",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_companies_TypeContactId",
                schema: "Company",
                table: "companies",
                column: "TypeContactId");

            migrationBuilder.CreateIndex(
                name: "IX_company_activities_ActivitySectorId",
                schema: "Company",
                table: "company_activities",
                column: "ActivitySectorId");

            migrationBuilder.CreateIndex(
                name: "IX_company_typecontacts_TypeConvertTo",
                schema: "Company",
                table: "company_typecontacts",
                column: "TypeConvertTo");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyValue_CompanyId",
                schema: "Company",
                table: "CompanyValue",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "company_activities",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "CompanyValue",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "ActivitySectors",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "companies",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "company_typecontacts",
                schema: "Company");
        }
    }
}
