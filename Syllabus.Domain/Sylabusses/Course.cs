namespace Syllabus.Domain.Sylabusses
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } //data Structures in C
        public string Code { get; set; } //CS101"
        public int Year { get; set; } // 1, 2, or 3
        public int Semester { get; set; } // 1 or 2
        public int LectureHours { get; set; }
        public int SeminarHours { get; set; }
        public int LabHours { get; set; }
        public int PracticeHours { get; set; }
        public int TotalHours => LectureHours + SeminarHours + LabHours + PracticeHours;
        public int Credits { get; set; } //credits
        public EvaluationMethod Evaluation { get; set; }
        public CourseType Type { get; set; }
        public string? ElectiveGroup { get; set; } // 'Elective I', 'Elective II', or null

        public CourseDetail? Detail { get; set; }
    }
}