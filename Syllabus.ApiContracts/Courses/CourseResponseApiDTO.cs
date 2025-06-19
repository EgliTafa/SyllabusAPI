using Syllabus.Domain.Sylabusses;
using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Courses
{
    /// <summary>
    /// Response model representing the details of a course.
    /// </summary>
    public class CourseResponseApiDTO
    {
        /// <summary>
        /// The unique identifier of the course.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// The title of the course.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = default!;

        /// <summary>
        /// The code of the course (e.g., CS101).
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = default!;

        /// <summary>
        /// The semester in which the course is taught.
        /// </summary>
        [JsonPropertyName("semester")]
        public int Semester { get; set; }

        /// <summary>
        /// The number of credits assigned to the course.
        /// </summary>
        [JsonPropertyName("credits")]
        public int Credits { get; set; }

        /// <summary>
        /// Number of lecture hours for the course.
        /// </summary>
        [JsonPropertyName("lectureHours")]
        public int LectureHours { get; set; }

        /// <summary>
        /// Number of seminar hours for the course.
        /// </summary>
        [JsonPropertyName("seminarHours")]
        public int SeminarHours { get; set; }

        /// <summary>
        /// Number of lab hours for the course.
        /// </summary>
        [JsonPropertyName("labHours")]
        public int LabHours { get; set; }

        /// <summary>
        /// Number of practice hours for the course.
        /// </summary>
        [JsonPropertyName("practiceHours")]
        public int PracticeHours { get; set; }

        /// <summary>
        /// The academic program this course belongs to.
        /// </summary>
        [JsonPropertyName("academicProgram")]
        public string? AcademicProgram { get; set; }

        /// <summary>
        /// The academic year for this course (e.g., "2023-2024").
        /// </summary>
        [JsonPropertyName("academicYear")]
        public string? AcademicYear { get; set; }

        /// <summary>
        /// The academic year (1, 2, or 3) in which the course is taught.
        /// </summary>
        [JsonPropertyName("year")]
        public int Year { get; set; }

        /// <summary>
        /// The language in which the course is taught.
        /// </summary>
        [JsonPropertyName("language")]
        public string? Language { get; set; }

        /// <summary>
        /// The label describing the course type.
        /// </summary>
        [JsonPropertyName("courseTypeLabel")]
        public string? CourseTypeLabel { get; set; }

        /// <summary>
        /// The ethics code associated with the course.
        /// </summary>
        [JsonPropertyName("ethicsCode")]
        public string? EthicsCode { get; set; }

        /// <summary>
        /// The method of examination for the course.
        /// </summary>
        [JsonPropertyName("examMethod")]
        public string? ExamMethod { get; set; }

        /// <summary>
        /// The format of teaching for the course.
        /// </summary>
        [JsonPropertyName("teachingFormat")]
        public string? TeachingFormat { get; set; }

        /// <summary>
        /// The teaching plan for the course.
        /// </summary>
        [JsonPropertyName("teachingPlan")]
        public TeachingPlan? TeachingPlan { get; set; }

        /// <summary>
        /// The evaluation breakdown for the course.
        /// </summary>
        [JsonPropertyName("evaluationBreakdown")]
        public EvaluationBreakdown? EvaluationBreakdown { get; set; }

        /// <summary>
        /// The objective of the course.
        /// </summary>
        [JsonPropertyName("objective")]
        public string? Objective { get; set; }

        /// <summary>
        /// The key concepts covered in the course.
        /// </summary>
        [JsonPropertyName("keyConcepts")]
        public string? KeyConcepts { get; set; }

        /// <summary>
        /// The prerequisites for the course.
        /// </summary>
        [JsonPropertyName("prerequisites")]
        public string? Prerequisites { get; set; }

        /// <summary>
        /// The skills acquired through the course.
        /// </summary>
        [JsonPropertyName("skillsAcquired")]
        public string? SkillsAcquired { get; set; }

        /// <summary>
        /// The name of the course responsible.
        /// </summary>
        [JsonPropertyName("courseResponsible")]
        public string? CourseResponsible { get; set; }

        /// <summary>
        /// The list of topics covered in the course.
        /// </summary>
        [JsonPropertyName("topics")]
        public List<TopicResponseApiDTO>? Topics { get; set; }

        /// <summary>
        /// The elective group for this course ('Elective I', 'Elective II', or null).
        /// </summary>
        [JsonPropertyName("electiveGroup")]
        public string? ElectiveGroup { get; set; }
    }
}
