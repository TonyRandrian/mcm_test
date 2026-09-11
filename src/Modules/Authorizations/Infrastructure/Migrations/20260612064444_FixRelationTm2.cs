using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Authorizations.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationTm2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_teammember_teammembers_TeamMemberId",
                schema: "authorizations",
                table: "role_teammember");

            migrationBuilder.AddForeignKey(
                name: "FK_role_teammember_teammembers_TeamMemberId",
                schema: "authorizations",
                table: "role_teammember",
                column: "TeamMemberId",
                principalSchema: "authorizations",
                principalTable: "teammembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_teammember_teammembers_TeamMemberId",
                schema: "authorizations",
                table: "role_teammember");

            migrationBuilder.AddForeignKey(
                name: "FK_role_teammember_teammembers_TeamMemberId",
                schema: "authorizations",
                table: "role_teammember",
                column: "TeamMemberId",
                principalSchema: "authorizations",
                principalTable: "teammembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
