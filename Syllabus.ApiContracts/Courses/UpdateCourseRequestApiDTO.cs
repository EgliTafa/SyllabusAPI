using Syllabus.Domain.Sylabusses;

namespace Syllabus.ApiContracts.Courses
{
    public class UpdateCourseRequestApiDTO
    {
        public int CourseId { get; set; }

        public string Title { get; set; } = default!;
        public string Code { get; set; } = default!;
        public int Semester { get; set; }
        public int LectureHours { get; set; }
        public int SeminarHours { get; set; }
        public int LabHours { get; set; }
        public int Credits { get; set; }
        public EvaluationMethod Evaluation { get; set; }
        public CourseType Type { get; set; }
    }
}
