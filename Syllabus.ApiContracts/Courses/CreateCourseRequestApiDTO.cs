using Syllabus.Domain.Sylabusses;

namespace Syllabus.ApiContracts.Courses
{
    /// <summary>
    /// Request model for creating a new course.
    /// </summary>
    public class CreateCourseRequestApiDTO
    {
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
        /// Number of lecture hours for the course.
        /// </summary>
        public int LectureHours { get; set; }

        /// <summary>
        /// Number of seminar hours for the course.
        /// </summary>
        public int SeminarHours { get; set; }

        /// <summary>
        /// Number of lab hours for the course.
        /// </summary>
        public int LabHours { get; set; }

        /// <summary>
        /// Number of credits assigned to the course.
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
        /// The ID of the syllabus to which this course belongs.
        /// </summary>
        public int SyllabusId { get; set; }

        /// <summary>
        /// Optional course details to be created along with the course.
        /// </summary>
        public CourseDetailRequestApiDTO? Detail { get; set; }
    }
}
