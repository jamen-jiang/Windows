using Microsoft.EntityFrameworkCore.Migrations;

namespace Windows.Admin.Infrastructure.EFCore.Migrations
{
    public partial class UpdateFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Extension",
                table: "File",
                newName: "ExtensionTest");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExtensionTest",
                table: "File",
                newName: "Extension");
        }
    }
}
