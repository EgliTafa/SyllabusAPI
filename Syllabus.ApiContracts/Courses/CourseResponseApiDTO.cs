using Syllabus.Domain.Sylabusses;

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
        public int Id { get; set; }

        /// <summary>
        /// The title of the course.
        /// </summary>
        public string Title { get; set; } = default!;

        /// <summary>
        /// The code of the course (e.g., CS101).
        /// </summary>
        public string Code { get; set; } = default!;

        /// <summary>
        /// The semester in which the course is taught.
        /// </summary>
        public int Semester { get; set; }

        /// <summary>
        /// The number of credits assigned to the course.
        /// </summary>
        public int Credits { get; set; }

        /// <summary>
        /// The academic program this course belongs to.
        /// </summary>
        public string? AcademicProgram { get; set; }

        /// <summary>
        /// The academic year for this course (e.g., "2023-2024").
        /// </summary>
        public string? AcademicYear { get; set; }

        /// <summary>
        /// The academic year (1, 2, or 3) in which the course is taught.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// The language in which the course is taught.
        /// </summary>
        public string? Language { get; set; }

        /// <summary>
        /// The label describing the course type.
        /// </summary>
        public string? CourseTypeLabel { get; set; }

        /// <summary>
        /// The ethics code associated with the course.
        /// </summary>
        public string? EthicsCode { get; set; }

        /// <summary>
        /// The method of examination for the course.
        /// </summary>
        public string? ExamMethod { get; set; }

        /// <summary>
        /// The format of teaching for the course.
        /// </summary>
        public string? TeachingFormat { get; set; }

        /// <summary>
        /// The teaching plan for the course.
        /// </summary>
        public TeachingPlan? TeachingPlan { get; set; }

        /// <summary>
        /// The evaluation breakdown for the course.
        /// </summary>
        public EvaluationBreakdown? EvaluationBreakdown { get; set; }

        /// <summary>
        /// The objective of the course.
        /// </summary>
        public string? Objective { get; set; }

        /// <summary>
        /// The key concepts covered in the course.
        /// </summary>
        public string? KeyConcepts { get; set; }

        /// <summary>
        /// The prerequisites for the course.
        /// </summary>
        public string? Prerequisites { get; set; }

        /// <summary>
        /// The skills acquired through the course.
        /// </summary>
        public string? SkillsAcquired { get; set; }

        /// <summary>
        /// The name of the course responsible.
        /// </summary>
        public string? CourseResponsible { get; set; }

        /// <summary>
        /// The list of topics covered in the course.
        /// </summary>
        public List<TopicResponseApiDTO>? Topics { get; set; }

        /// <summary>
        /// The elective group for this course ('Elective I', 'Elective II', or null).
        /// </summary>
        public string? ElectiveGroup { get; set; }
    }
}
