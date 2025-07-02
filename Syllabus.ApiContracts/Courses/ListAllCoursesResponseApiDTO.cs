using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Courses
{
    /// <summary>
    /// Response model for listing all courses with pagination.
    /// </summary>
    public class ListAllCoursesResponseApiDTO
    {
        /// <summary>
        /// List of courses for the current page
        /// </summary>
        [JsonPropertyName("courses")]
        public List<CourseResponseApiDTO> Courses { get; set; } = new List<CourseResponseApiDTO>();

        /// <summary>
        /// Total number of courses
        /// </summary>
        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        /// <summary>
        /// Current page number
        /// </summary>
        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; }

        /// <summary>
        /// Number of items per page
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        /// <summary>
        /// Total number of pages
        /// </summary>
        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }

        /// <summary>
        /// Whether there is a next page
        /// </summary>
        [JsonPropertyName("hasNextPage")]
        public bool HasNextPage { get; set; }

        /// <summary>
        /// Whether there is a previous page
        /// </summary>
        [JsonPropertyName("hasPreviousPage")]
        public bool HasPreviousPage { get; set; }

        // Legacy property for backward compatibility
        [JsonPropertyName("allCourses")]
        public List<CourseResponseApiDTO> AllCourses 
        { 
            get => Courses; 
            set => Courses = value; 
        }
    }
}
