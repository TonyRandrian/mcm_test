using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Property.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateBy",
                schema: "properties",
                table: "property_definition",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreateBy",
                schema: "properties",
                table: "category_value",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreateBy",
                schema: "properties",
                table: "category_entity",
                newName: "UpdatedBy");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "properties",
                table: "property_definition",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "properties",
                table: "property_definition",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "properties",
                table: "category_value",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "properties",
                table: "category_value",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "properties",
                table: "category_entity",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                schema: "properties",
                table: "category_entity",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "properties",
                table: "property_definition");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "properties",
                table: "property_definition");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "properties",
                table: "category_value");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "properties",
                table: "category_value");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "properties",
                table: "category_entity");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "properties",
                table: "category_entity");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "properties",
                table: "property_definition",
                newName: "CreateBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "properties",
                table: "category_value",
                newName: "CreateBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "properties",
                table: "category_entity",
                newName: "CreateBy");
        }
    }
}
