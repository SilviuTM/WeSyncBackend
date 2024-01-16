using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeSyncBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedVirtualPathToFisier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Fisiers",
                newName: "VirtualPath");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Fisiers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Fisiers");

            migrationBuilder.RenameColumn(
                name: "VirtualPath",
                table: "Fisiers",
                newName: "Name");
        }
    }
}
