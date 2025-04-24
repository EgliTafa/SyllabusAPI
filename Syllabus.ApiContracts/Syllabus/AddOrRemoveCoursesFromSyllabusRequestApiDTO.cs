namespace Syllabus.ApiContracts.Syllabus
{
    public class AddOrRemoveCoursesFromSyllabusRequestApiDTO
    {
        public int SyllabusId { get; set; }
        public List<int> CourseIdsToAdd { get; set; } = new();
        public List<int> CourseIdsToRemove { get; set; } = new();
    }
}
