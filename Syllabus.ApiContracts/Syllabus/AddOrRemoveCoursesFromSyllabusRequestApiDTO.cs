using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Syllabus
{
    /// <summary>
    /// Request to add or remove courses from a syllabus.
    /// </summary>
    public class AddOrRemoveCoursesFromSyllabusRequestApiDTO
    {
        /// <summary>
        /// The ID of the syllabus to update.
        /// </summary>
        [JsonPropertyName("syllabusId")]
        public int SyllabusId { get; set; }

        /// <summary>
        /// List of course IDs to add to the syllabus.
        /// </summary>
        [JsonPropertyName("courseIdsToAdd")]
        public List<int> CourseIdsToAdd { get; set; } = new();

        /// <summary>
        /// List of course IDs to remove from the syllabus.
        /// </summary>
        [JsonPropertyName("courseIdsToRemove")]
        public List<int> CourseIdsToRemove { get; set; } = new();
    }
}
