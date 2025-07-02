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
    }
}
