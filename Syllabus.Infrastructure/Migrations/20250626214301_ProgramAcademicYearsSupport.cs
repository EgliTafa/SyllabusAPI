using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Syllabus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProgramAcademicYearsSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Syllabuses_Programs_ProgramId",
                table: "Syllabuses");

            migrationBuilder.DropIndex(
                name: "IX_Syllabuses_ProgramId",
                table: "Syllabuses");

            migrationBuilder.DropIndex(
                name: "IX_Programs_Name_AcademicYear",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Syllabuses");

            migrationBuilder.DropColumn(
                name: "AcademicYear",
                table: "Programs");

            migrationBuilder.AddColumn<int>(
                name: "ProgramAcademicYearId",
                table: "Syllabuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProgramAcademicYears",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    AcademicYear = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramAcademicYears", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramAcademicYears_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Syllabuses_ProgramAcademicYearId",
                table: "Syllabuses",
                column: "ProgramAcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramAcademicYears_ProgramId_AcademicYear",
                table: "ProgramAcademicYears",
                columns: new[] { "ProgramId", "AcademicYear" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Syllabuses_ProgramAcademicYears_ProgramAcademicYearId",
                table: "Syllabuses",
                column: "ProgramAcademicYearId",
                principalTable: "ProgramAcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Syllabuses_ProgramAcademicYears_ProgramAcademicYearId",
                table: "Syllabuses");

            migrationBuilder.DropTable(
                name: "ProgramAcademicYears");

            migrationBuilder.DropIndex(
                name: "IX_Syllabuses_ProgramAcademicYearId",
                table: "Syllabuses");

            migrationBuilder.DropColumn(
                name: "ProgramAcademicYearId",
                table: "Syllabuses");

            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "Syllabuses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcademicYear",
                table: "Programs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Syllabuses_ProgramId",
                table: "Syllabuses",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_Name_AcademicYear",
                table: "Programs",
                columns: new[] { "Name", "AcademicYear" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Syllabuses_Programs_ProgramId",
                table: "Syllabuses",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
