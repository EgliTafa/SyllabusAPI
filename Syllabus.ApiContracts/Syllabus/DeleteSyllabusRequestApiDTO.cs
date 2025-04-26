namespace Syllabus.ApiContracts.Syllabus
{
    /// <summary>
    /// Request to delete a syllabus by its ID.
    /// </summary>
    public class DeleteSyllabusRequestApiDTO
    {
        /// <summary>
        /// The ID of the syllabus to delete.
        /// </summary>
        public int SyllabusId { get; set; }
    }
}
