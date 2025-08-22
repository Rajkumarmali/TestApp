using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApp.Migrations
{
    /// <inheritdoc />
    public partial class initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StuCourse_Course_CourseId",
                table: "StuCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_StuCourse_Students_StudentId",
                table: "StuCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StuCourse",
                table: "StuCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.RenameTable(
                name: "StuCourse",
                newName: "StuCourses");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "Courses");

            migrationBuilder.RenameIndex(
                name: "IX_StuCourse_StudentId",
                table: "StuCourses",
                newName: "IX_StuCourses_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StuCourse_CourseId",
                table: "StuCourses",
                newName: "IX_StuCourses_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StuCourses",
                table: "StuCourses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuCourses_Courses_CourseId",
                table: "StuCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StuCourses_Students_StudentId",
                table: "StuCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StuCourses_Courses_CourseId",
                table: "StuCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StuCourses_Students_StudentId",
                table: "StuCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StuCourses",
                table: "StuCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "StuCourses",
                newName: "StuCourse");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Course");

            migrationBuilder.RenameIndex(
                name: "IX_StuCourses_StudentId",
                table: "StuCourse",
                newName: "IX_StuCourse_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StuCourses_CourseId",
                table: "StuCourse",
                newName: "IX_StuCourse_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StuCourse",
                table: "StuCourse",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StuCourse_Course_CourseId",
                table: "StuCourse",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StuCourse_Students_StudentId",
                table: "StuCourse",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
