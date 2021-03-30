using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreSimpam.Model.Migrations
{
    public partial class AddRoleScreenTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleScreen",
                schema: "Common",
                columns: table => new
                {
                    RoleScreenID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleID = table.Column<long>(type: "bigint", nullable: false),
                    ScreenID = table.Column<long>(type: "bigint", nullable: false),
                    ReadFlag = table.Column<bool>(type: "bit", nullable: false),
                    WriteFlag = table.Column<bool>(type: "bit", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleScreen", x => x.RoleScreenID);
                    table.ForeignKey(
                        name: "FK_RoleScreen_Roles_RoleID",
                        column: x => x.RoleID,
                        principalSchema: "Common",
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleScreen_Screen_ScreenID",
                        column: x => x.ScreenID,
                        principalSchema: "Common",
                        principalTable: "Screen",
                        principalColumn: "ScreenID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleScreen_RoleID",
                schema: "Common",
                table: "RoleScreen",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_RoleScreen_ScreenID",
                schema: "Common",
                table: "RoleScreen",
                column: "ScreenID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleScreen",
                schema: "Common");
        }
    }
}
