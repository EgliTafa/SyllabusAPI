namespace Syllabus.ApiContracts.Courses
{
    /// <summary>
    /// Response model containing a list of all courses.
    /// </summary>
    public class ListAllCoursesResponseApiDTO
    {
        /// <summary>
        /// The list of all courses.
        /// </summary>
        public List<CourseResponseApiDTO> AllCourses { get; set; } = new();
    }
}
