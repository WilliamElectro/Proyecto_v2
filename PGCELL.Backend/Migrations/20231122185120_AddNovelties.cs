﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PGCELL.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddNovelties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Novelties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Novelties", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Novelties_Name",
                table: "Novelties",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Novelties");
        }
    }
}
