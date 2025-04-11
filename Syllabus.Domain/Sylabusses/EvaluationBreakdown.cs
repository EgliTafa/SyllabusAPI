using Microsoft.EntityFrameworkCore;

namespace Syllabus.Domain.Sylabusses
{
    [Owned]
    public class EvaluationBreakdown
    {
        public int ParticipationPercent { get; set; }
        public int Test1Percent { get; set; }
        public int Test2Percent { get; set; }
        public int Test3Percent { get; set; }
        public int FinalExamPercent { get; set; }
    }

}
