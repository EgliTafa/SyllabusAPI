using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Syllabus
{
    /// <summary>
    /// Request to retrieve a syllabus by its ID.
    /// </summary>
    public class GetSyllabusByIdRequestApiDTO
    {
        /// <summary>
        /// The ID of the syllabus to retrieve.
        /// </summary>
        [JsonPropertyName("syllabusId")]
        public int SyllabusId { get; set; }
    }
}
