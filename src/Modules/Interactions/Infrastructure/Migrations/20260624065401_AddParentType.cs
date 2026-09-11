using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Interactions.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddParentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                schema: "interactions",
                table: "interaction_interactionTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_interaction_interactionTypes_ParentId",
                schema: "interactions",
                table: "interaction_interactionTypes",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_interaction_interactionTypes_interaction_interactionTypes_P~",
                schema: "interactions",
                table: "interaction_interactionTypes",
                column: "ParentId",
                principalSchema: "interactions",
                principalTable: "interaction_interactionTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_interaction_interactionTypes_interaction_interactionTypes_P~",
                schema: "interactions",
                table: "interaction_interactionTypes");

            migrationBuilder.DropIndex(
                name: "IX_interaction_interactionTypes_ParentId",
                schema: "interactions",
                table: "interaction_interactionTypes");

            migrationBuilder.DropColumn(
                name: "ParentId",
                schema: "interactions",
                table: "interaction_interactionTypes");
        }
    }
}
