using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Syllabus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MoveAcademicYearToProgram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcademicYear",
                table: "Syllabuses");

            migrationBuilder.AddColumn<string>(
                name: "AcademicYear",
                table: "Programs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_Name_AcademicYear",
                table: "Programs",
                columns: new[] { "Name", "AcademicYear" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Programs_Name_AcademicYear",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "AcademicYear",
                table: "Programs");

            migrationBuilder.AddColumn<string>(
                name: "AcademicYear",
                table: "Syllabuses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
