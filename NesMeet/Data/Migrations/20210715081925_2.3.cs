using Microsoft.EntityFrameworkCore.Migrations;

namespace NesMeet.Migrations
{
    public partial class _23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sesmester",
                table: "Classrooms",
                newName: "Semester");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Semester",
                table: "Classrooms",
                newName: "Sesmester");
        }
    }
}
