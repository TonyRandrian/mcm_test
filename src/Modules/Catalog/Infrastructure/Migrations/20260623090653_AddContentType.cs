using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddContentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverPicture_ContentType",
                schema: "Catalog",
                table: "catalog_services",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                schema: "Catalog",
                table: "catalog_service_images",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverPicture_ContentType",
                schema: "Catalog",
                table: "catalog_products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                schema: "Catalog",
                table: "catalog_product_images",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverPicture_ContentType",
                schema: "Catalog",
                table: "catalog_services");

            migrationBuilder.DropColumn(
                name: "ContentType",
                schema: "Catalog",
                table: "catalog_service_images");

            migrationBuilder.DropColumn(
                name: "CoverPicture_ContentType",
                schema: "Catalog",
                table: "catalog_products");

            migrationBuilder.DropColumn(
                name: "ContentType",
                schema: "Catalog",
                table: "catalog_product_images");
        }
    }
}
