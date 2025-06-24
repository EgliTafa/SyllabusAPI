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
        public int DepartmentId { get; set; }
    }
} 