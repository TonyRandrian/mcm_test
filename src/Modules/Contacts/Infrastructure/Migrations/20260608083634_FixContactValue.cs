using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Contacts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixContactValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactValue_contacts_CompanyId",
                schema: "contacts",
                table: "ContactValue");

            migrationBuilder.DropIndex(
                name: "IX_ContactValue_CompanyId",
                schema: "contacts",
                table: "ContactValue");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "contacts",
                table: "ContactValue");

            migrationBuilder.CreateIndex(
                name: "IX_ContactValue_ContactId",
                schema: "contacts",
                table: "ContactValue",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactValue_contacts_ContactId",
                schema: "contacts",
                table: "ContactValue",
                column: "ContactId",
                principalSchema: "contacts",
                principalTable: "contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactValue_contacts_ContactId",
                schema: "contacts",
                table: "ContactValue");

            migrationBuilder.DropIndex(
                name: "IX_ContactValue_ContactId",
                schema: "contacts",
                table: "ContactValue");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "contacts",
                table: "ContactValue",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
    }
}
