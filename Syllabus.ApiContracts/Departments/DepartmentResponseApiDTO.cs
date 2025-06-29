using Syllabus.ApiContracts.Programs;

namespace Syllabus.ApiContracts.Departments
{
    public class DepartmentResponseApiDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<ProgramResponseApiDTO> Programs { get; set; } = new List<ProgramResponseApiDTO>();
    }
} 