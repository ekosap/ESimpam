using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreSimpam.Model.Migrations
{
    public partial class addUserTableWithRoleID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RoleID",
                schema: "Common",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                schema: "Common",
                table: "Users",
                column: "RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleID",
                schema: "Common",
                table: "Users",
                column: "RoleID",
                principalSchema: "Common",
                principalTable: "Roles",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleID",
                schema: "Common",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleID",
                schema: "Common",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleID",
                schema: "Common",
                table: "Users");
        }
    }
}
