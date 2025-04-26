namespace Syllabus.ApiContracts.Courses
{
    /// <summary>
    /// Request model for retrieving a course by its ID.
    /// </summary>
    public class GetCourseByIdRequest
    {
        /// <summary>
        /// The ID of the course to retrieve.
        /// </summary>
        public int CourseId { get; set; }
    }
}
