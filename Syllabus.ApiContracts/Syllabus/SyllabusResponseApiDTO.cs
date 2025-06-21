using Syllabus.ApiContracts.Courses;
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
        /// The academic year for this syllabus (e.g., "2023-2024").
        /// </summary>
        [JsonPropertyName("academicYear")]
        public string AcademicYear { get; set; } = default!;

        /// <summary>
        /// The list of courses in the syllabus.
        /// </summary>
        [JsonPropertyName("courses")]
        public List<CourseResponseApiDTO> Courses { get; set; } = new();
    }
}
