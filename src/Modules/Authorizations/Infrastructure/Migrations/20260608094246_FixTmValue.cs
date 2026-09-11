using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Authorizations.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixTmValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_teammember_values_teammembers_TeamMemberId",
                schema: "authorizations",
                table: "teammember_values");

            migrationBuilder.DropPrimaryKey(
                name: "PK_teammember_values",
                schema: "authorizations",
                table: "teammember_values");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                schema: "authorizations",
                table: "teammember_values");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "authorizations",
                table: "teammember_values");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "authorizations",
                table: "teammember_values");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "authorizations",
                table: "teammember_values");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "authorizations",
                table: "teammember_values");

            migrationBuilder.RenameTable(
                name: "teammember_values",
                schema: "authorizations",
                newName: "TeamMemberValue",
                newSchema: "authorizations");

            migrationBuilder.RenameIndex(
                name: "IX_teammember_values_TeamMemberId",
                schema: "authorizations",
                table: "TeamMemberValue",
                newName: "IX_TeamMemberValue_TeamMemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamMemberValue",
                schema: "authorizations",
                table: "TeamMemberValue",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMemberValue_teammembers_TeamMemberId",
                schema: "authorizations",
                table: "TeamMemberValue",
                column: "TeamMemberId",
                principalSchema: "authorizations",
                principalTable: "teammembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMemberValue_teammembers_TeamMemberId",
                schema: "authorizations",
                table: "TeamMemberValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamMemberValue",
                schema: "authorizations",
                table: "TeamMemberValue");

            migrationBuilder.RenameTable(
                name: "TeamMemberValue",
                schema: "authorizations",
                newName: "teammember_values",
                newSchema: "authorizations");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMemberValue_TeamMemberId",
                schema: "authorizations",
                table: "teammember_values",
                newName: "IX_teammember_values_TeamMemberId");

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                schema: "authorizations",
                table: "teammember_values",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "authorizations",
                table: "teammember_values",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "authorizations",
                table: "teammember_values",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "authorizations",
                table: "teammember_values",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "authorizations",
                table: "teammember_values",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_teammember_values",
                schema: "authorizations",
                table: "teammember_values",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_teammember_values_teammembers_TeamMemberId",
                schema: "authorizations",
                table: "teammember_values",
                column: "TeamMemberId",
                principalSchema: "authorizations",
                principalTable: "teammembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
