namespace Syllabus.ApiContracts.Syllabus
{
    /// <summary>
    /// Request to update the name of a syllabus.
    /// </summary>
    public class UpdateSyllabusRequestApiDTO
    {
        /// <summary>
        /// The ID of the syllabus to update.
        /// </summary>
        public int SyllabusId { get; set; }

        /// <summary>
        /// The new name of the syllabus.
        /// </summary>
        public string Name { get; set; } = default!;
    }
}
