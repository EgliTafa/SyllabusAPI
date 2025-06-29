using Syllabus.ApiContracts.Courses;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The ID of the program academic year this syllabus belongs to.
        /// </summary>
        [JsonPropertyName("programAcademicYearId")]
        public int ProgramAcademicYearId { get; set; }

        /// <summary>
        /// The list of courses included in the syllabus.
        /// </summary>
        [JsonPropertyName("courses")]
        public List<CreateCourseRequestApiDTO> Courses { get; set; } = new();
    }
}
