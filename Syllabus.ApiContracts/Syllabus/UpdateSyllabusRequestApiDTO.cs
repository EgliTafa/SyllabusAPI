namespace Syllabus.ApiContracts.Syllabus
{
    /// <summary>
    /// Request to update a syllabus.
    /// </summary>
    public class UpdateSyllabusRequestApiDTO
    {
        /// <summary>
        /// The ID of the syllabus to update.
        /// </summary>
        public int SyllabusId { get; set; }

        /// <summary>
        /// The new name for the syllabus.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The academic year for this syllabus (e.g., "2023-2024").
        /// </summary>
        public string AcademicYear { get; set; } = string.Empty;
    }
}
