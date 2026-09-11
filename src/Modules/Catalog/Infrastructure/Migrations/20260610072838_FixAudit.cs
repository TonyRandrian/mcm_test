using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateBy",
                schema: "Catalog",
                table: "catalog_services",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreateBy",
                schema: "Catalog",
                table: "catalog_service_categories",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreateBy",
                schema: "Catalog",
                table: "catalog_products",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreateBy",
                schema: "Catalog",
                table: "catalog_product_categories",
                newName: "UpdatedBy");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "Catalog",
                table: "catalog_services",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "Catalog",
                table: "catalog_services",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "Catalog",
                table: "catalog_service_categories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "Catalog",
                table: "catalog_service_categories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "Catalog",
                table: "catalog_products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "Catalog",
                table: "catalog_products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "Catalog",
                table: "catalog_product_categories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "Catalog",
                table: "catalog_product_categories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Catalog",
                table: "catalog_services");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Catalog",
                table: "catalog_services");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Catalog",
                table: "catalog_service_categories");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Catalog",
                table: "catalog_service_categories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Catalog",
                table: "catalog_products");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Catalog",
                table: "catalog_products");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Catalog",
                table: "catalog_product_categories");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Catalog",
                table: "catalog_product_categories");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "Catalog",
                table: "catalog_services",
                newName: "CreateBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "Catalog",
                table: "catalog_service_categories",
                newName: "CreateBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "Catalog",
                table: "catalog_products",
                newName: "CreateBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "Catalog",
                table: "catalog_product_categories",
                newName: "CreateBy");
        }
    }
}
