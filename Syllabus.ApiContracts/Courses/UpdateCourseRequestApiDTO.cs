using Syllabus.Domain.Sylabusses;

namespace Syllabus.ApiContracts.Courses
{
    /// <summary>
    /// Request model for updating an existing course.
    /// </summary>
    public class UpdateCourseRequestApiDTO
    {
        /// <summary>
        /// The ID of the course to update.
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// The updated title of the course.
        /// </summary>
        public string Title { get; set; } = default!;

        /// <summary>
        /// The updated code of the course.
        /// </summary>
        public string Code { get; set; } = default!;

        /// <summary>
        /// The semester in which the course is taught.
        /// </summary>
        public int Semester { get; set; }

        /// <summary>
        /// Updated number of lecture hours.
        /// </summary>
        public int LectureHours { get; set; }

        /// <summary>
        /// Updated number of seminar hours.
        /// </summary>
        public int SeminarHours { get; set; }

        /// <summary>
        /// Updated number of lab hours.
        /// </summary>
        public int LabHours { get; set; }

        /// <summary>
        /// Updated number of credits for the course.
        /// </summary>
        public int Credits { get; set; }

        /// <summary>
        /// The evaluation method used for this course.
        /// </summary>
        public EvaluationMethod Evaluation { get; set; }

        /// <summary>
        /// The type of the course (e.g., Mandatory, Elective).
        /// </summary>
        public CourseType Type { get; set; }

        /// <summary>
        /// The academic year (1, 2, or 3) in which the course is taught.
        /// </summary>
        public int Year { get; set; }
    }
}
