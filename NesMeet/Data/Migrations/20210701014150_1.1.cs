using Microsoft.EntityFrameworkCore.Migrations;

namespace NesMeet.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TraineeClassroom_AspNetUsers_TraineeId",
                table: "TraineeClassroom");

            migrationBuilder.DropForeignKey(
                name: "FK_TraineeClassroom_Classrooms_ClassroomId",
                table: "TraineeClassroom");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TraineeClassroom",
                table: "TraineeClassroom");

            migrationBuilder.RenameTable(
                name: "TraineeClassroom",
                newName: "TraineeClassrooms");

            migrationBuilder.RenameColumn(
                name: "DepartmentID",
                table: "AspNetUsers",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_DepartmentID",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_TraineeClassroom_TraineeId",
                table: "TraineeClassrooms",
                newName: "IX_TraineeClassrooms_TraineeId");

            migrationBuilder.RenameIndex(
                name: "IX_TraineeClassroom_ClassroomId",
                table: "TraineeClassrooms",
                newName: "IX_TraineeClassrooms_ClassroomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TraineeClassrooms",
                table: "TraineeClassrooms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TraineeClassrooms_AspNetUsers_TraineeId",
                table: "TraineeClassrooms",
                column: "TraineeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TraineeClassrooms_Classrooms_ClassroomId",
                table: "TraineeClassrooms",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TraineeClassrooms_AspNetUsers_TraineeId",
                table: "TraineeClassrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_TraineeClassrooms_Classrooms_ClassroomId",
                table: "TraineeClassrooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TraineeClassrooms",
                table: "TraineeClassrooms");

            migrationBuilder.RenameTable(
                name: "TraineeClassrooms",
                newName: "TraineeClassroom");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "AspNetUsers",
                newName: "DepartmentID");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_DepartmentID");

            migrationBuilder.RenameIndex(
                name: "IX_TraineeClassrooms_TraineeId",
                table: "TraineeClassroom",
                newName: "IX_TraineeClassroom_TraineeId");

            migrationBuilder.RenameIndex(
                name: "IX_TraineeClassrooms_ClassroomId",
                table: "TraineeClassroom",
                newName: "IX_TraineeClassroom_ClassroomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TraineeClassroom",
                table: "TraineeClassroom",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentID",
                table: "AspNetUsers",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TraineeClassroom_AspNetUsers_TraineeId",
                table: "TraineeClassroom",
                column: "TraineeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TraineeClassroom_Classrooms_ClassroomId",
                table: "TraineeClassroom",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
