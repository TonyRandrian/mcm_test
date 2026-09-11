using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Contacts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateBy",
                schema: "contacts",
                table: "contacts",
                newName: "UpdatedBy");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "contacts",
                table: "contacts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "contacts",
                table: "contacts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "contacts",
                table: "contacts");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "contacts",
                table: "contacts");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "contacts",
                table: "contacts",
                newName: "CreateBy");
        }
    }
}
