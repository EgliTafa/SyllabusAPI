using Syllabus.ApiContracts.Courses;

namespace Syllabus.ApiContracts.Syllabus
{
    /// <summary>
    /// Response model for a syllabus.
    /// </summary>
    public class SyllabusResponseApiDTO
    {
        /// <summary>
        /// The ID of the syllabus.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the syllabus.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// The list of courses in the syllabus.
        /// </summary>
        public List<CourseResponseApiDTO> Courses { get; set; } = new();
    }
}
