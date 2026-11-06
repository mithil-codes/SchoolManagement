using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TeacherForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "ClassSubjects",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClassTeacherId",
                table: "Classes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MarkedByTeacherId",
                table: "Attendances",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjects_TeacherId",
                table: "ClassSubjects",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ClassTeacherId",
                table: "Classes",
                column: "ClassTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_MarkedByTeacherId",
                table: "Attendances",
                column: "MarkedByTeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_AspNetUsers_MarkedByTeacherId",
                table: "Attendances",
                column: "MarkedByTeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_AspNetUsers_ClassTeacherId",
                table: "Classes",
                column: "ClassTeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubjects_AspNetUsers_TeacherId",
                table: "ClassSubjects",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_AspNetUsers_MarkedByTeacherId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_AspNetUsers_ClassTeacherId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubjects_AspNetUsers_TeacherId",
                table: "ClassSubjects");

            migrationBuilder.DropIndex(
                name: "IX_ClassSubjects_TeacherId",
                table: "ClassSubjects");

            migrationBuilder.DropIndex(
                name: "IX_Classes_ClassTeacherId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_MarkedByTeacherId",
                table: "Attendances");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "ClassSubjects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClassTeacherId",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MarkedByTeacherId",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
