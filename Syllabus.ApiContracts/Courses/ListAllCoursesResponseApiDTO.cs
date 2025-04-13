namespace Syllabus.ApiContracts.Courses
{
    public class ListAllCoursesResponseApiDTO
    {
        public List<CourseResponseApiDTO> AllCourses { get; set; } = new List<CourseResponseApiDTO>();
    }
}
