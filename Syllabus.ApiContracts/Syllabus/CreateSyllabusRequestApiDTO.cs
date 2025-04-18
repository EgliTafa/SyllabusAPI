using Syllabus.ApiContracts.Courses;

namespace Syllabus.ApiContracts.Syllabus
{
    public class CreateSyllabusRequestApiDTO
    {
        public string Name { get; set; } = string.Empty;
        public List<CreateCourseRequestApiDTO> Courses { get; set; } = new();
    }
}
