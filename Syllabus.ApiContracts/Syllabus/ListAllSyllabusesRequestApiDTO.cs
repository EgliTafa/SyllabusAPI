using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Syllabus
{
    /// <summary>
    /// Request model for listing all syllabuses with pagination and sorting.
    /// </summary>
    public class ListAllSyllabusesRequestApiDTO
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
        public int PageSize { get; set; } = 12;

        /// <summary>
        /// Sort field (name, program, department, academicYear)
        /// </summary>
        [JsonPropertyName("sortBy")]
        public string? SortBy { get; set; }

        /// <summary>
        /// Sort direction (asc, desc)
        /// </summary>
        [JsonPropertyName("sortDirection")]
        public string? SortDirection { get; set; } = "asc";

        /// <summary>
        /// Search term for filtering syllabuses
        /// </summary>
        [JsonPropertyName("searchTerm")]
        public string? SearchTerm { get; set; }

        /// <summary>
        /// Filter by department ID
        /// </summary>
        [JsonPropertyName("departmentId")]
        public int? DepartmentId { get; set; }

        /// <summary>
        /// Filter by program ID
        /// </summary>
        [JsonPropertyName("programId")]
        public int? ProgramId { get; set; }

        /// <summary>
        /// Filter by academic year
        /// </summary>
        [JsonPropertyName("academicYear")]
        public string? AcademicYear { get; set; }
    }
}
