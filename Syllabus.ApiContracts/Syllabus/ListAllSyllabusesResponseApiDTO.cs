using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Syllabus
{
    /// <summary>
    /// Response model for listing all syllabuses with pagination.
    /// </summary>
    public class ListAllSyllabusesResponseApiDTO
    {
        /// <summary>
        /// List of syllabuses for the current page
        /// </summary>
        [JsonPropertyName("syllabuses")]
        public List<SyllabusResponseApiDTO> Syllabuses { get; set; } = new List<SyllabusResponseApiDTO>();

        /// <summary>
        /// Total number of syllabuses
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
        [JsonPropertyName("allSyllabuses")]
        public List<SyllabusResponseApiDTO> AllSyllabuses 
        { 
            get => Syllabuses; 
            set => Syllabuses = value; 
        }
    }
}
