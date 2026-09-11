using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Company.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyFilter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                schema: "Company",
                table: "company_typecontacts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "Company",
                table: "companies",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                schema: "Company",
                table: "company_typecontacts");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "Company",
                table: "companies");
        }
    }
}
