using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Contacts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddContentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image_ContentType",
                schema: "contacts",
                table: "contacts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image_ContentType",
                schema: "contacts",
                table: "contacts");
        }
    }
}
