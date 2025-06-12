using Syllabus.ApiContracts.Courses;

namespace Syllabus.ApiContracts.Syllabus
{
    /// <summary>
    /// Request to create a new syllabus.
    /// </summary>
    public class CreateSyllabusRequestApiDTO
    {
        /// <summary>
        /// The name of the syllabus.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The academic year for this syllabus (e.g., "2023-2024").
        /// </summary>
        public string AcademicYear { get; set; } = string.Empty;

        /// <summary>
        /// The list of courses included in the syllabus.
        /// </summary>
        public List<CreateCourseRequestApiDTO> Courses { get; set; } = new();
    }
}
