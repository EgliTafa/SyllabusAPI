namespace Syllabus.Domain.Sylabusses
{
    public class Sylabus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProgramAcademicYearId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        public ProgramAcademicYear ProgramAcademicYear { get; set; } = null!;
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
