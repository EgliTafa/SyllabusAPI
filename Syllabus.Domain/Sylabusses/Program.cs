namespace Syllabus.Domain.Sylabusses
{
    public class Program
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AcademicYear { get; set; } = string.Empty; // Format: "2020-2023", "2021-2024", etc.
        public int DepartmentId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        public Department Department { get; set; } = null!;
        public ICollection<Sylabus> Syllabuses { get; set; } = new List<Sylabus>();
    }
} 