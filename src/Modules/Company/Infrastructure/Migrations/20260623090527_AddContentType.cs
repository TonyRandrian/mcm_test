using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Company.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddContentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Logo_ContentType",
                schema: "Company",
                table: "companies",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo_ContentType",
                schema: "Company",
                table: "companies");
        }
    }
}
