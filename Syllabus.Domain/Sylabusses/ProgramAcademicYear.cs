namespace Syllabus.Domain.Sylabusses
{
    public class ProgramAcademicYear
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public string AcademicYear { get; set; } = string.Empty;
        public Program Program { get; set; } = null!;
        public ICollection<Sylabus> Syllabuses { get; set; } = new List<Sylabus>();
    }
} 