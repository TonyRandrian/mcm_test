using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Authorizations.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixAuthorization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_teammembers_roles_RoleId",
                schema: "authorizations",
                table: "teammembers");

            migrationBuilder.DropIndex(
                name: "IX_teammembers_RoleId",
                schema: "authorizations",
                table: "teammembers");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "authorizations",
                table: "teammembers",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreateBy",
                schema: "authorizations",
                table: "teammembers",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "CreateBy",
                schema: "authorizations",
                table: "roles",
                newName: "UpdatedBy");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "authorizations",
                table: "teammembers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "authorizations",
                table: "roles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "authorizations",
                table: "roles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "role_teammember",
                schema: "authorizations",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamMemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_teammember", x => new { x.RoleId, x.TeamMemberId });
                    table.ForeignKey(
                        name: "FK_role_teammember_roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "authorizations",
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_teammember_teammembers_TeamMemberId",
                        column: x => x.TeamMemberId,
                        principalSchema: "authorizations",
                        principalTable: "teammembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_role_teammember_TeamMemberId",
                schema: "authorizations",
                table: "role_teammember",
                column: "TeamMemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "role_teammember",
                schema: "authorizations");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "authorizations",
                table: "teammembers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "authorizations",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "authorizations",
                table: "roles");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "authorizations",
                table: "teammembers",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                schema: "authorizations",
                table: "teammembers",
                newName: "CreateBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "authorizations",
                table: "roles",
                newName: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_teammembers_RoleId",
                schema: "authorizations",
                table: "teammembers",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_teammembers_roles_RoleId",
                schema: "authorizations",
                table: "teammembers",
                column: "RoleId",
                principalSchema: "authorizations",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
