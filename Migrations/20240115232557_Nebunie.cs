using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeSyncBackend.Migrations
{
    /// <inheritdoc />
    public partial class Nebunie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FisierUser",
                columns: table => new
                {
                    FisierListId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FisierUser", x => new { x.FisierListId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_FisierUser_Fisiers_FisierListId",
                        column: x => x.FisierListId,
                        principalTable: "Fisiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FisierUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FisierUser_UsersId",
                table: "FisierUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FisierUser");
        }
    }
}
