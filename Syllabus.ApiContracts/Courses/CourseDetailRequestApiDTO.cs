using Syllabus.Domain.Sylabusses;

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
        public string AcademicProgram { get; set; } = default!;

        /// <summary>
        /// The academic year for this course.
        /// </summary>
        public string AcademicYear { get; set; } = default!;

        /// <summary>
        /// The language in which the course is taught.
        /// </summary>
        public string Language { get; set; } = default!;

        /// <summary>
        /// The label describing the course type.
        /// </summary>
        public string CourseTypeLabel { get; set; } = default!;

        /// <summary>
        /// The ethics code associated with the course.
        /// </summary>
        public string EthicsCode { get; set; } = default!;

        /// <summary>
        /// The method of examination for the course.
        /// </summary>
        public string ExamMethod { get; set; } = default!;

        /// <summary>
        /// The format of teaching for the course.
        /// </summary>
        public string TeachingFormat { get; set; } = default!;

        /// <summary>
        /// The number of credits for the course.
        /// </summary>
        public int Credits { get; set; }

        /// <summary>
        /// The teaching plan for the course.
        /// </summary>
        public TeachingPlan TeachingPlan { get; set; } = new();

        /// <summary>
        /// The evaluation breakdown for the course.
        /// </summary>
        public EvaluationBreakdown EvaluationBreakdown { get; set; } = new();

        /// <summary>
        /// The objective of the course.
        /// </summary>
        public string Objective { get; set; } = default!;

        /// <summary>
        /// The key concepts covered in the course.
        /// </summary>
        public string KeyConcepts { get; set; } = default!;

        /// <summary>
        /// The prerequisites for the course.
        /// </summary>
        public string Prerequisites { get; set; } = default!;

        /// <summary>
        /// The skills acquired through the course.
        /// </summary>
        public string SkillsAcquired { get; set; } = default!;

        /// <summary>
        /// The list of topics covered in the course.
        /// </summary>
        public List<TopicRequestApiDTO>? Topics { get; set; }

        /// <summary>
        /// The name of the course responsible.
        /// </summary>
        public string? CourseResponsible { get; set; }
    }
} 