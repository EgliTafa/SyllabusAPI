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
        /// The detailed learning objectives of the course.
        /// </summary>
        public string? DetailObjective { get; set; }

        /// <summary>
        /// The list of main topics covered in the course.
        /// </summary>
        public List<string>? Topics { get; set; }
    }
}
