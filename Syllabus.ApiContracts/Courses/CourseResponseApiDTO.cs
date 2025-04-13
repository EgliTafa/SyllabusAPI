namespace Syllabus.ApiContracts.Courses
{
    public class CourseResponseApiDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int Semester { get; set; }
        public int Credits { get; set; }

        public string? DetailObjective { get; set; }
        public List<string>? Topics { get; set; }
    }
}
