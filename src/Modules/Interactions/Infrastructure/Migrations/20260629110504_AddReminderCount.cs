using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mcm.Interactions.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReminderCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Reminder_ReminderCount",
                schema: "interactions",
                table: "interactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reminder_ReminderCount",
                schema: "interactions",
                table: "interactions");
        }
    }
}
