namespace Syllabus.ApiContracts.Courses
{
    /// <summary>
    /// Request model for deleting a course.
    /// </summary>
    public class DeleteCourseRequestApiDTO
    {
        /// <summary>
        /// The ID of the course to delete.
        /// </summary>
        public int CourseId { get; set; }
    }
}
