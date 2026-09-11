using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Company.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCompany1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateBy",
                schema: "Company",
                table: "company_typecontacts",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreateBy",
                schema: "Company",
                table: "companies",
                newName: "UpdatedBy");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "Company",
                table: "company_typecontacts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "Company",
                table: "company_typecontacts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "Company",
                table: "companies",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "Company",
                table: "companies",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LeaderId",
                schema: "Company",
                table: "companies",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Company",
                table: "company_typecontacts");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Company",
                table: "company_typecontacts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Company",
                table: "companies");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Company",
                table: "companies");

            migrationBuilder.DropColumn(
                name: "LeaderId",
                schema: "Company",
                table: "companies");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "Company",
                table: "company_typecontacts",
                newName: "CreateBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "Company",
                table: "companies",
                newName: "CreateBy");
        }
    }
}
