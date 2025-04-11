namespace Syllabus.Domain.Sylabusses
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } //data Structures in C
        public string Code { get; set; } //CS101"
        public int Semester { get; set; } // 1 to 6
        public int LectureHours { get; set; }
        public int SeminarHours { get; set; }
        public int LabHours { get; set; }
        public int TotalHours => LectureHours + SeminarHours + LabHours;
        public int Credits { get; set; } //credits
        public EvaluationMethod Evaluation { get; set; }
        public CourseType Type { get; set; }

        public CourseDetail? Detail { get; set; }
    }
}