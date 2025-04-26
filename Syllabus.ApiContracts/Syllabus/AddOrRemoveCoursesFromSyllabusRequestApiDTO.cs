namespace Syllabus.ApiContracts.Syllabus
{
    /// <summary>
    /// Request to add or remove courses from a syllabus.
    /// </summary>
    public class AddOrRemoveCoursesFromSyllabusRequestApiDTO
    {
        /// <summary>
        /// The ID of the syllabus to update.
        /// </summary>
        public int SyllabusId { get; set; }

        /// <summary>
        /// List of course IDs to add to the syllabus.
        /// </summary>
        public List<int> CourseIdsToAdd { get; set; } = new();

        /// <summary>
        /// List of course IDs to remove from the syllabus.
        /// </summary>
        public List<int> CourseIdsToRemove { get; set; } = new();
    }
}
