namespace Syllabus.Domain.Sylabusses
{
    public class CourseDetail
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }

        public string AcademicProgram { get; set; } // "Master Shkencor..."
        public string AcademicYear { get; set; } // "2023-2025"
        public string Language { get; set; } // "Anglisht"
        public string CourseTypeLabel { get; set; } // "C / Me Zgjedhje"
        public string EthicsCode { get; set; }

        public string ExamMethod { get; set; } // "Provim"
        public string TeachingFormat { get; set; } // "Me Shkrim"

        public int Credits { get; set; }

        public TeachingPlan TeachingPlan { get; set; } = new();
        public EvaluationBreakdown EvaluationBreakdown { get; set; } = new();

        public string Objective { get; set; }
        public string KeyConcepts { get; set; }
        public string Prerequisites { get; set; }
        public string SkillsAcquired { get; set; }

        // Pergjegjesi i lëndës
        public string? CourseResponsible { get; set; }

        public ICollection<Topic>? Topics { get; set; } = new List<Topic>();
    }

}
