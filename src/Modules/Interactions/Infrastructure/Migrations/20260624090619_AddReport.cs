using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Interactions.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_interaction_reports_interactions_InteractionId",
                schema: "interactions",
                table: "interaction_reports");

            migrationBuilder.DropIndex(
                name: "IX_interaction_reports_InteractionId",
                schema: "interactions",
                table: "interaction_reports");

            migrationBuilder.CreateIndex(
                name: "IX_interactions_ReportId",
                schema: "interactions",
                table: "interactions",
                column: "ReportId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_interactions_interaction_reports_ReportId",
                schema: "interactions",
                table: "interactions",
                column: "ReportId",
                principalSchema: "interactions",
                principalTable: "interaction_reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_interactions_interaction_reports_ReportId",
                schema: "interactions",
                table: "interactions");

            migrationBuilder.DropIndex(
                name: "IX_interactions_ReportId",
                schema: "interactions",
                table: "interactions");

            migrationBuilder.CreateIndex(
                name: "IX_interaction_reports_InteractionId",
                schema: "interactions",
                table: "interaction_reports",
                column: "InteractionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_interaction_reports_interactions_InteractionId",
                schema: "interactions",
                table: "interaction_reports",
                column: "InteractionId",
                principalSchema: "interactions",
                principalTable: "interactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
