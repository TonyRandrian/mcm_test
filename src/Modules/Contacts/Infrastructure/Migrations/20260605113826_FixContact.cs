using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Contacts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contact_values_contacts_ContactId",
                schema: "contacts",
                table: "contact_values");

            migrationBuilder.DropPrimaryKey(
                name: "PK_contact_values",
                schema: "contacts",
                table: "contact_values");

            migrationBuilder.DropIndex(
                name: "IX_contact_values_ContactId",
                schema: "contacts",
                table: "contact_values");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "contacts",
                table: "contact_values");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "contacts",
                table: "contact_values");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "contacts",
                table: "contact_values");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "contacts",
                table: "contact_values");

            migrationBuilder.RenameTable(
                name: "contact_values",
                schema: "contacts",
                newName: "ContactValue",
                newSchema: "contacts");

            migrationBuilder.RenameColumn(
                name: "CreateBy",
                schema: "contacts",
                table: "ContactValue",
                newName: "CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactValue",
                schema: "contacts",
                table: "ContactValue",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContactValue_CompanyId",
                schema: "contacts",
                table: "ContactValue",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactValue_contacts_CompanyId",
                schema: "contacts",
                table: "ContactValue",
                column: "CompanyId",
                principalSchema: "contacts",
                principalTable: "contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactValue_contacts_CompanyId",
                schema: "contacts",
                table: "ContactValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactValue",
                schema: "contacts",
                table: "ContactValue");

            migrationBuilder.DropIndex(
                name: "IX_ContactValue_CompanyId",
                schema: "contacts",
                table: "ContactValue");

            migrationBuilder.RenameTable(
                name: "ContactValue",
                schema: "contacts",
                newName: "contact_values",
                newSchema: "contacts");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                schema: "contacts",
                table: "contact_values",
                newName: "CreateBy");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "contacts",
                table: "contact_values",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "contacts",
                table: "contact_values",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "contacts",
                table: "contact_values",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "contacts",
                table: "contact_values",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_contact_values",
                schema: "contacts",
                table: "contact_values",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_values_ContactId",
                schema: "contacts",
                table: "contact_values",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_contact_values_contacts_ContactId",
                schema: "contacts",
                table: "contact_values",
                column: "ContactId",
                principalSchema: "contacts",
                principalTable: "contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
