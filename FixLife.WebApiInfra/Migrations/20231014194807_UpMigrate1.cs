using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FixLife.WebApiInfra.Migrations
{
    /// <inheritdoc />
    public partial class UpMigrate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LearnTimeId",
                table: "DayOfWeek",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DayOfWeek_LearnTimeId",
                table: "DayOfWeek",
                column: "LearnTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DayOfWeek_LearnTimes_LearnTimeId",
                table: "DayOfWeek",
                column: "LearnTimeId",
                principalTable: "LearnTimes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayOfWeek_LearnTimes_LearnTimeId",
                table: "DayOfWeek");

            migrationBuilder.DropIndex(
                name: "IX_DayOfWeek_LearnTimeId",
                table: "DayOfWeek");

            migrationBuilder.DropColumn(
                name: "LearnTimeId",
                table: "DayOfWeek");
        }
    }
}
