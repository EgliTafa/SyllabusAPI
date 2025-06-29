using System.ComponentModel.DataAnnotations;

namespace Syllabus.ApiContracts.Programs
{
    public class UpdateProgramRequestApiDTO
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        [RegularExpression(@"^\d{4}-\d{4}$", ErrorMessage = "Academic year must be in format YYYY-YYYY")]
        public string AcademicYear { get; set; } = string.Empty;
        
        [Required]
        public int DepartmentId { get; set; }
    }
} 