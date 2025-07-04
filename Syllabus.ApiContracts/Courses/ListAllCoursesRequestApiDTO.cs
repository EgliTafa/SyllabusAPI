using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Courses
{
    /// <summary>
    /// Request model for listing all courses with pagination and sorting.
    /// </summary>
    public class ListAllCoursesRequestApiDTO
    {
        /// <summary>
        /// Page number (1-based)
        /// </summary>
        [JsonPropertyName("page")]
        public int Page { get; set; } = 1;

        /// <summary>
        /// Number of items per page
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Sort field (title, code, semester, credits, year)
        /// </summary>
        [JsonPropertyName("sortBy")]
        public string? SortBy { get; set; }

        /// <summary>
        /// Sort direction (asc, desc)
        /// </summary>
        [JsonPropertyName("sortDirection")]
        public string? SortDirection { get; set; } = "asc";

        /// <summary>
        /// Search term for filtering courses
        /// </summary>
        [JsonPropertyName("searchTerm")]
        public string? SearchTerm { get; set; }
    }
}
