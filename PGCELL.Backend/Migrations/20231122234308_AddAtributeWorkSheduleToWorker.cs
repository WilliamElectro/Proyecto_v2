using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PGCELL.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddAtributeWorkSheduleToWorker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkScheduleId",
                table: "Workers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_WorkScheduleId",
                table: "Workers",
                column: "WorkScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_WorkSchedules_WorkScheduleId",
                table: "Workers",
                column: "WorkScheduleId",
                principalTable: "WorkSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_WorkSchedules_WorkScheduleId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_WorkScheduleId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "WorkScheduleId",
                table: "Workers");
        }
    }
}
