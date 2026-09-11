using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Mcm.Interactions.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInteraction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "interactions");

            migrationBuilder.CreateTable(
                name: "interaction_interactionTypes",
                schema: "interactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LabelColor = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interaction_interactionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "interaction_typeFields",
                schema: "interactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    InteractionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interaction_typeFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_interaction_typeFields_interaction_interactionTypes_Interac~",
                        column: x => x.InteractionTypeId,
                        principalSchema: "interactions",
                        principalTable: "interaction_interactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "interactions",
                schema: "interactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IsDone = table.Column<bool>(type: "boolean", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    startDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    endDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    reminder_type = table.Column<int>(type: "integer", nullable: false),
                    reminder_value = table.Column<double>(type: "double precision", nullable: false),
                    reminder_repeat = table.Column<int>(type: "integer", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReportId = table.Column<Guid>(type: "uuid", nullable: true),
                    TypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_interactions_interaction_interactionTypes_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "interactions",
                        principalTable: "interaction_interactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "interaction_attachments",
                schema: "interactions",
                columns: table => new
                {
                    InteractionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StorageType = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    AlternativeText = table.Column<string>(type: "text", nullable: false),
                    FileType = table.Column<int>(type: "integer", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interaction_attachments", x => new { x.InteractionId, x.Id });
                    table.ForeignKey(
                        name: "FK_interaction_attachments_interactions_InteractionId",
                        column: x => x.InteractionId,
                        principalSchema: "interactions",
                        principalTable: "interactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "interaction_contacts",
                schema: "interactions",
                columns: table => new
                {
                    InteractionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContactId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interaction_contacts", x => new { x.InteractionId, x.ContactId });
                    table.ForeignKey(
                        name: "FK_interaction_contacts_interactions_InteractionId",
                        column: x => x.InteractionId,
                        principalSchema: "interactions",
                        principalTable: "interactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "interaction_fieldsValues",
                schema: "interactions",
                columns: table => new
                {
                    InteractionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeFieldId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interaction_fieldsValues", x => new { x.InteractionId, x.Id });
                    table.ForeignKey(
                        name: "FK_interaction_fieldsValues_interactions_InteractionId",
                        column: x => x.InteractionId,
                        principalSchema: "interactions",
                        principalTable: "interactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "interaction_Members",
                schema: "interactions",
                columns: table => new
                {
                    InteractionId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamMemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interaction_Members", x => new { x.InteractionId, x.TeamMemberId });
                    table.ForeignKey(
                        name: "FK_interaction_Members_interactions_InteractionId",
                        column: x => x.InteractionId,
                        principalSchema: "interactions",
                        principalTable: "interactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "interaction_reports",
                schema: "interactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ActionPlan = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    startDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    endDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    InteractionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interaction_reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_interaction_reports_interactions_InteractionId",
                        column: x => x.InteractionId,
                        principalSchema: "interactions",
                        principalTable: "interactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "report_attachments",
                schema: "interactions",
                columns: table => new
                {
                    ReportId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StorageType = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    AlternativeText = table.Column<string>(type: "text", nullable: false),
                    FileType = table.Column<int>(type: "integer", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_attachments", x => new { x.ReportId, x.Id });
                    table.ForeignKey(
                        name: "FK_report_attachments_interaction_reports_ReportId",
                        column: x => x.ReportId,
                        principalSchema: "interactions",
                        principalTable: "interaction_reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "report_contacts",
                schema: "interactions",
                columns: table => new
                {
                    ReportId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContactId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_contacts", x => new { x.ReportId, x.ContactId });
                    table.ForeignKey(
                        name: "FK_report_contacts_interaction_reports_ReportId",
                        column: x => x.ReportId,
                        principalSchema: "interactions",
                        principalTable: "interaction_reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "report_members",
                schema: "interactions",
                columns: table => new
                {
                    ReportId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamMemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_members", x => new { x.ReportId, x.TeamMemberId });
                    table.ForeignKey(
                        name: "FK_report_members_interaction_reports_ReportId",
                        column: x => x.ReportId,
                        principalSchema: "interactions",
                        principalTable: "interaction_reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_interaction_reports_InteractionId",
                schema: "interactions",
                table: "interaction_reports",
                column: "InteractionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_interaction_typeFields_InteractionTypeId",
                schema: "interactions",
                table: "interaction_typeFields",
                column: "InteractionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_interactions_TypeId",
                schema: "interactions",
                table: "interactions",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "interaction_attachments",
                schema: "interactions");

            migrationBuilder.DropTable(
                name: "interaction_contacts",
                schema: "interactions");

            migrationBuilder.DropTable(
                name: "interaction_fieldsValues",
                schema: "interactions");

            migrationBuilder.DropTable(
                name: "interaction_Members",
                schema: "interactions");

            migrationBuilder.DropTable(
                name: "interaction_typeFields",
                schema: "interactions");

            migrationBuilder.DropTable(
                name: "report_attachments",
                schema: "interactions");

            migrationBuilder.DropTable(
                name: "report_contacts",
                schema: "interactions");

            migrationBuilder.DropTable(
                name: "report_members",
                schema: "interactions");

            migrationBuilder.DropTable(
                name: "interaction_reports",
                schema: "interactions");

            migrationBuilder.DropTable(
                name: "interactions",
                schema: "interactions");

            migrationBuilder.DropTable(
                name: "interaction_interactionTypes",
                schema: "interactions");
        }
    }
}
