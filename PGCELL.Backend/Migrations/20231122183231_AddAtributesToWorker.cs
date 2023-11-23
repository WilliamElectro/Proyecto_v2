using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PGCELL.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddAtributesToWorker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Workers_Name",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Workers");

            migrationBuilder.AddColumn<string>(
                name: "Document",
                table: "Workers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Workers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Workers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ModalityId",
                table: "Workers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Workers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_Document",
                table: "Workers",
                column: "Document",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_ModalityId",
                table: "Workers",
                column: "ModalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_UserId",
                table: "Workers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_AspNetUsers_UserId",
                table: "Workers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Modalities_ModalityId",
                table: "Workers",
                column: "ModalityId",
                principalTable: "Modalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_AspNetUsers_UserId",
                table: "Workers");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Modalities_ModalityId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_Document",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_ModalityId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_UserId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "Document",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "ModalityId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Workers");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Workers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_Name",
                table: "Workers",
                column: "Name",
                unique: true);
        }
    }
}
