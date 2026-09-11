using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Authorizations.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationTm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_teammember_roles_RoleId",
                schema: "authorizations",
                table: "role_teammember");

            migrationBuilder.AddForeignKey(
                name: "FK_role_teammember_roles_RoleId",
                schema: "authorizations",
                table: "role_teammember",
                column: "RoleId",
                principalSchema: "authorizations",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_teammember_roles_RoleId",
                schema: "authorizations",
                table: "role_teammember");

            migrationBuilder.AddForeignKey(
                name: "FK_role_teammember_roles_RoleId",
                schema: "authorizations",
                table: "role_teammember",
                column: "RoleId",
                principalSchema: "authorizations",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
