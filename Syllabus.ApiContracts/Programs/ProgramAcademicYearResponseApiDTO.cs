namespace Syllabus.ApiContracts.Programs
{
    public class ProgramAcademicYearResponseApiDTO
    {
        public int Id { get; set; }
        public string AcademicYear { get; set; } = string.Empty;
        public ProgramResponseApiDTO Program { get; set; } = new();
    }
} 