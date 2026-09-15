using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Authorizations.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRoleCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_role_company",
                schema: "authorizations",
                table: "role_company");

            migrationBuilder.DropIndex(
                name: "IX_role_company_RoleId",
                schema: "authorizations",
                table: "role_company");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "authorizations",
                table: "role_company");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role_company",
                schema: "authorizations",
                table: "role_company",
                columns: new[] { "RoleId", "CompanyId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_role_company",
                schema: "authorizations",
                table: "role_company");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "authorizations",
                table: "role_company",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_role_company",
                schema: "authorizations",
                table: "role_company",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_role_company_RoleId",
                schema: "authorizations",
                table: "role_company",
                column: "RoleId");
        }
    }
}
