using Syllabus.ApiContracts.Courses;
using Syllabus.ApiContracts.Programs;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// The name of the syllabus.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        /// <summary>
        /// The program this syllabus belongs to.
        /// </summary>
        [JsonPropertyName("program")]
        public ProgramResponseApiDTO Program { get; set; } = default!;

        /// <summary>
        /// The program academic year this syllabus belongs to.
        /// </summary>
        [JsonPropertyName("programAcademicYear")]
        public ProgramAcademicYearDTO ProgramAcademicYear { get; set; } = default!;

        /// <summary>
        /// The list of courses in the syllabus.
        /// </summary>
        [JsonPropertyName("courses")]
        public List<CourseResponseApiDTO> Courses { get; set; } = new();
    }
}
