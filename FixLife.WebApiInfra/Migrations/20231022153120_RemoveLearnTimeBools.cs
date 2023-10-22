using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FixLife.WebApiInfra.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLearnTimeBools : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWeekly",
                table: "LearnTimes");

            migrationBuilder.DropColumn(
                name: "IsYearly",
                table: "LearnTimes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWeekly",
                table: "LearnTimes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsYearly",
                table: "LearnTimes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
