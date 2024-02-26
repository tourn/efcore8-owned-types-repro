using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repro.Migrations
{
    /// <inheritdoc />
    public partial class TestAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffings_Staffings_StaffEntry<DefaultTasks>StaffingId",
                table: "Staffings");

            migrationBuilder.DropColumn(
                name: "StaffEntry<DefaultTasks>StaffingId",
                table: "Staffings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StaffEntry<DefaultTasks>StaffingId",
                table: "Staffings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffings_Staffings_StaffEntry<DefaultTasks>StaffingId",
                table: "Staffings",
                column: "StaffEntry<DefaultTasks>StaffingId",
                principalTable: "Staffings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
