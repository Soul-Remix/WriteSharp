using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WriteSharp.API.Migrations
{
    public partial class Update_WhiteLists_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "WhiteLists",
                newName: "Word");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Word",
                table: "WhiteLists",
                newName: "Text");
        }
    }
}
