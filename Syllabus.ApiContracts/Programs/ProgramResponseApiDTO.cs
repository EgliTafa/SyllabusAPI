using System;
using System.Collections.Generic;

namespace Syllabus.ApiContracts.Programs
{
    public class ProgramAcademicYearDTO
    {
        public int Id { get; set; }
        public string AcademicYear { get; set; } = string.Empty;
    }

    public class ProgramResponseApiDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<ProgramAcademicYearDTO> AcademicYears { get; set; } = new();
    }
} 