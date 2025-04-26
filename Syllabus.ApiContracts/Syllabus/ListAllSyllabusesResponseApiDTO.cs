namespace Syllabus.ApiContracts.Syllabus
{
    /// <summary>
    /// Response containing a list of all syllabuses.
    /// </summary>
    public class ListAllSyllabusesResponseApiDTO
    {
        /// <summary>
        /// List of syllabuses.
        /// </summary>
        public List<SyllabusResponseApiDTO> Syllabuses { get; set; } = new();
    }
}
