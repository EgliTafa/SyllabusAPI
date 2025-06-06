using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Syllabus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAcademicYearField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcademicYear",
                table: "Syllabuses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcademicYear",
                table: "Syllabuses");
        }
    }
}
