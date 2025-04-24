using Syllabus.ApiContracts.Courses;

namespace Syllabus.ApiContracts.Syllabus
{
    public class SyllabusResponseApiDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public List<CourseResponseApiDTO> Courses { get; set; } = new();
    }
}
