using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_relation_product_categories_catalog_product_categories_Cate~",
                schema: "Catalog",
                table: "relation_product_categories");

            migrationBuilder.DropForeignKey(
                name: "FK_relation_product_categories_catalog_products_ProductId",
                schema: "Catalog",
                table: "relation_product_categories");

            migrationBuilder.AddForeignKey(
                name: "FK_relation_product_categories_catalog_product_categories_Cate~",
                schema: "Catalog",
                table: "relation_product_categories",
                column: "CategoryId",
                principalSchema: "Catalog",
                principalTable: "catalog_product_categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_relation_product_categories_catalog_products_ProductId",
                schema: "Catalog",
                table: "relation_product_categories",
                column: "ProductId",
                principalSchema: "Catalog",
                principalTable: "catalog_products",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_relation_product_categories_catalog_product_categories_Cate~",
                schema: "Catalog",
                table: "relation_product_categories");

            migrationBuilder.DropForeignKey(
                name: "FK_relation_product_categories_catalog_products_ProductId",
                schema: "Catalog",
                table: "relation_product_categories");

            migrationBuilder.AddForeignKey(
                name: "FK_relation_product_categories_catalog_product_categories_Cate~",
                schema: "Catalog",
                table: "relation_product_categories",
                column: "CategoryId",
                principalSchema: "Catalog",
                principalTable: "catalog_product_categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_relation_product_categories_catalog_products_ProductId",
                schema: "Catalog",
                table: "relation_product_categories",
                column: "ProductId",
                principalSchema: "Catalog",
                principalTable: "catalog_products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
