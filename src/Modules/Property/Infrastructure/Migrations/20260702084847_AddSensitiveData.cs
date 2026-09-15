using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Property.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSensitiveData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSensitive",
                schema: "properties",
                table: "property_definition",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSensitive",
                schema: "properties",
                table: "property_definition");
        }
    }
}
