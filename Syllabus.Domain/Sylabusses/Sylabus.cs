namespace Syllabus.Domain.Sylabusses
{
    public class Sylabus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
