using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FixLife.WebApiInfra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPlan_ClientUser_UsersId",
                table: "UserPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPlan_Plans_PlansId",
                table: "UserPlan");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "UserPlan",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "PlansId",
                table: "UserPlan",
                newName: "PlanId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPlan_UsersId",
                table: "UserPlan",
                newName: "IX_UserPlan_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPlan_PlansId",
                table: "UserPlan",
                newName: "IX_UserPlan_PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlan_ClientUser_UserId",
                table: "UserPlan",
                column: "UserId",
                principalTable: "ClientUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlan_Plans_PlanId",
                table: "UserPlan",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPlan_ClientUser_UserId",
                table: "UserPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPlan_Plans_PlanId",
                table: "UserPlan");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserPlan",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "PlanId",
                table: "UserPlan",
                newName: "PlansId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPlan_UserId",
                table: "UserPlan",
                newName: "IX_UserPlan_UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPlan_PlanId",
                table: "UserPlan",
                newName: "IX_UserPlan_PlansId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlan_ClientUser_UsersId",
                table: "UserPlan",
                column: "UsersId",
                principalTable: "ClientUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlan_Plans_PlansId",
                table: "UserPlan",
                column: "PlansId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
