using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Syllabus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CourseDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    AcademicProgram = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AcademicYear = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CourseTypeLabel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EthicsCode = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ExamMethod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TeachingFormat = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Credits = table.Column<int>(type: "int", nullable: false),
                    LectureHours = table.Column<int>(type: "int", nullable: false),
                    LabHours = table.Column<int>(type: "int", nullable: false),
                    PracticeHours = table.Column<int>(type: "int", nullable: false),
                    ExerciseHours = table.Column<int>(type: "int", nullable: false),
                    WeeklyHours = table.Column<int>(type: "int", nullable: false),
                    IndividualStudyHours = table.Column<int>(type: "int", nullable: false),
                    Participation = table.Column<int>(type: "int", nullable: false),
                    Test1 = table.Column<int>(type: "int", nullable: false),
                    Test2 = table.Column<int>(type: "int", nullable: false),
                    Test3 = table.Column<int>(type: "int", nullable: false),
                    FinalExam = table.Column<int>(type: "int", nullable: false),
                    Objective = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyConcepts = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prerequisites = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillsAcquired = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseDetails_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseTopics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseDetailId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTopics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseTopics_CourseDetails_CourseDetailId",
                        column: x => x.CourseDetailId,
                        principalTable: "CourseDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseDetails_CourseId",
                table: "CourseDetails",
                column: "CourseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseTopics_CourseDetailId",
                table: "CourseTopics",
                column: "CourseDetailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseTopics");

            migrationBuilder.DropTable(
                name: "CourseDetails");
        }
    }
}
