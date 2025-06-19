using Syllabus.Domain.Sylabusses;
using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Courses
{
    /// <summary>
    /// Request model for creating or updating course details.
    /// </summary>
    public class CourseDetailRequestApiDTO
    {
        /// <summary>
        /// The academic program this course belongs to.
        /// </summary>
        [JsonPropertyName("academicProgram")]
        public string AcademicProgram { get; set; } = default!;

        /// <summary>
        /// The academic year for this course.
        /// </summary>
        [JsonPropertyName("academicYear")]
        public string AcademicYear { get; set; } = default!;

        /// <summary>
        /// The language in which the course is taught.
        /// </summary>
        [JsonPropertyName("language")]
        public string Language { get; set; } = default!;

        /// <summary>
        /// The label describing the course type.
        /// </summary>
        [JsonPropertyName("courseTypeLabel")]
        public string CourseTypeLabel { get; set; } = default!;

        /// <summary>
        /// The ethics code associated with the course.
        /// </summary>
        [JsonPropertyName("ethicsCode")]
        public string EthicsCode { get; set; } = default!;

        /// <summary>
        /// The method of examination for the course.
        /// </summary>
        [JsonPropertyName("examMethod")]
        public string ExamMethod { get; set; } = default!;

        /// <summary>
        /// The format of teaching for the course.
        /// </summary>
        [JsonPropertyName("teachingFormat")]
        public string TeachingFormat { get; set; } = default!;

        /// <summary>
        /// The number of credits for the course.
        /// </summary>
        [JsonPropertyName("credits")]
        public int Credits { get; set; }

        /// <summary>
        /// The teaching plan for the course.
        /// </summary>
        [JsonPropertyName("teachingPlan")]
        public TeachingPlan TeachingPlan { get; set; } = new();

        /// <summary>
        /// The evaluation breakdown for the course.
        /// </summary>
        [JsonPropertyName("evaluationBreakdown")]
        public EvaluationBreakdown EvaluationBreakdown { get; set; } = new();

        /// <summary>
        /// The objective of the course.
        /// </summary>
        [JsonPropertyName("objective")]
        public string Objective { get; set; } = default!;

        /// <summary>
        /// The key concepts covered in the course.
        /// </summary>
        [JsonPropertyName("keyConcepts")]
        public string KeyConcepts { get; set; } = default!;

        /// <summary>
        /// The prerequisites for the course.
        /// </summary>
        [JsonPropertyName("prerequisites")]
        public string Prerequisites { get; set; } = default!;

        /// <summary>
        /// The skills acquired through the course.
        /// </summary>
        [JsonPropertyName("skillsAcquired")]
        public string SkillsAcquired { get; set; } = default!;

        /// <summary>
        /// The list of topics covered in the course.
        /// </summary>
        [JsonPropertyName("topics")]
        public List<TopicRequestApiDTO>? Topics { get; set; }

        /// <summary>
        /// The name of the course responsible.
        /// </summary>
        [JsonPropertyName("courseResponsible")]
        public string? CourseResponsible { get; set; }
    }
} 