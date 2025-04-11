using Microsoft.EntityFrameworkCore;

namespace Syllabus.Domain.Sylabusses
{
    [Owned]
    public class TeachingPlan
    {
        public int LectureHours { get; set; }
        public int LabHours { get; set; }
        public int PracticeHours { get; set; }
        public int ExerciseHours { get; set; }

        public int WeeklyHours { get; set; }
        public int IndividualStudyHours { get; set; }
    }

}
