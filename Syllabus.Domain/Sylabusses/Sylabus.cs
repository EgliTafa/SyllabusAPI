namespace Syllabus.Domain.Sylabusses
{
    public class Sylabus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AcademicYear { get; set; } // Format: "2023-2024"
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
