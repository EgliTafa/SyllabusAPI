using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Syllabus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCourseDetailMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WeeklyHours",
                table: "CourseDetails",
                newName: "TeachingPlan_WeeklyHours");

            migrationBuilder.RenameColumn(
                name: "Test3",
                table: "CourseDetails",
                newName: "EvaluationBreakdown_Test3Percent");

            migrationBuilder.RenameColumn(
                name: "Test2",
                table: "CourseDetails",
                newName: "EvaluationBreakdown_Test2Percent");

            migrationBuilder.RenameColumn(
                name: "Test1",
                table: "CourseDetails",
                newName: "EvaluationBreakdown_Test1Percent");

            migrationBuilder.RenameColumn(
                name: "PracticeHours",
                table: "CourseDetails",
                newName: "TeachingPlan_PracticeHours");

            migrationBuilder.RenameColumn(
                name: "Participation",
                table: "CourseDetails",
                newName: "EvaluationBreakdown_ParticipationPercent");

            migrationBuilder.RenameColumn(
                name: "LectureHours",
                table: "CourseDetails",
                newName: "TeachingPlan_LectureHours");

            migrationBuilder.RenameColumn(
                name: "LabHours",
                table: "CourseDetails",
                newName: "TeachingPlan_LabHours");

            migrationBuilder.RenameColumn(
                name: "IndividualStudyHours",
                table: "CourseDetails",
                newName: "TeachingPlan_IndividualStudyHours");

            migrationBuilder.RenameColumn(
                name: "FinalExam",
                table: "CourseDetails",
                newName: "EvaluationBreakdown_FinalExamPercent");

            migrationBuilder.RenameColumn(
                name: "ExerciseHours",
                table: "CourseDetails",
                newName: "TeachingPlan_ExerciseHours");

            migrationBuilder.AlterColumn<string>(
                name: "EthicsCode",
                table: "CourseDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "CourseResponsible",
                table: "CourseDetails",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AcademicYear",
                table: "CourseDetails",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "AcademicProgram",
                table: "CourseDetails",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TeachingPlan_WeeklyHours",
                table: "CourseDetails",
                newName: "WeeklyHours");

            migrationBuilder.RenameColumn(
                name: "TeachingPlan_PracticeHours",
                table: "CourseDetails",
                newName: "PracticeHours");

            migrationBuilder.RenameColumn(
                name: "TeachingPlan_LectureHours",
                table: "CourseDetails",
                newName: "LectureHours");

            migrationBuilder.RenameColumn(
                name: "TeachingPlan_LabHours",
                table: "CourseDetails",
                newName: "LabHours");

            migrationBuilder.RenameColumn(
                name: "TeachingPlan_IndividualStudyHours",
                table: "CourseDetails",
                newName: "IndividualStudyHours");

            migrationBuilder.RenameColumn(
                name: "TeachingPlan_ExerciseHours",
                table: "CourseDetails",
                newName: "ExerciseHours");

            migrationBuilder.RenameColumn(
                name: "EvaluationBreakdown_Test3Percent",
                table: "CourseDetails",
                newName: "Test3");

            migrationBuilder.RenameColumn(
                name: "EvaluationBreakdown_Test2Percent",
                table: "CourseDetails",
                newName: "Test2");

            migrationBuilder.RenameColumn(
                name: "EvaluationBreakdown_Test1Percent",
                table: "CourseDetails",
                newName: "Test1");

            migrationBuilder.RenameColumn(
                name: "EvaluationBreakdown_ParticipationPercent",
                table: "CourseDetails",
                newName: "Participation");

            migrationBuilder.RenameColumn(
                name: "EvaluationBreakdown_FinalExamPercent",
                table: "CourseDetails",
                newName: "FinalExam");

            migrationBuilder.AlterColumn<string>(
                name: "EthicsCode",
                table: "CourseDetails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CourseResponsible",
                table: "CourseDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AcademicYear",
                table: "CourseDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "AcademicProgram",
                table: "CourseDetails",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);
        }
    }
}
