using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Courses
{
    /// <summary>
    /// Request model for creating or updating course topics.
    /// </summary>
    public class TopicRequestApiDTO
    {
        /// <summary>
        /// The title of the topic.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = default!;

        /// <summary>
        /// The number of hours allocated for this topic.
        /// </summary>
        [JsonPropertyName("hours")]
        public int Hours { get; set; }

        /// <summary>
        /// Optional reference materials for this topic.
        /// </summary>
        [JsonPropertyName("reference")]
        public string? Reference { get; set; }
    }
}