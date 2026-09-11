using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Property.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_category_entity_category_value_CategoryId",
                schema: "properties",
                table: "category_entity");

            migrationBuilder.DropForeignKey(
                name: "FK_property_definition_category_value_CategoryId",
                schema: "properties",
                table: "property_definition");

            migrationBuilder.AddForeignKey(
                name: "FK_category_entity_category_value_CategoryId",
                schema: "properties",
                table: "category_entity",
                column: "CategoryId",
                principalSchema: "properties",
                principalTable: "category_value",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_property_definition_category_value_CategoryId",
                schema: "properties",
                table: "property_definition",
                column: "CategoryId",
                principalSchema: "properties",
                principalTable: "category_value",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_category_entity_category_value_CategoryId",
                schema: "properties",
                table: "category_entity");

            migrationBuilder.DropForeignKey(
                name: "FK_property_definition_category_value_CategoryId",
                schema: "properties",
                table: "property_definition");

            migrationBuilder.AddForeignKey(
                name: "FK_category_entity_category_value_CategoryId",
                schema: "properties",
                table: "category_entity",
                column: "CategoryId",
                principalSchema: "properties",
                principalTable: "category_value",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_property_definition_category_value_CategoryId",
                schema: "properties",
                table: "property_definition",
                column: "CategoryId",
                principalSchema: "properties",
                principalTable: "category_value",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
