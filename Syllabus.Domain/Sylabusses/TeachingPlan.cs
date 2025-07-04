using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Syllabus.Domain.Sylabusses
{
    [Owned]
    public class TeachingPlan
    {
        [JsonPropertyName("lectureHours")]
        public int LectureHours { get; set; }
        
        [JsonPropertyName("labHours")]
        public int LabHours { get; set; }
        
        [JsonPropertyName("practiceHours")]
        public int PracticeHours { get; set; }
        
        [JsonPropertyName("exerciseHours")]
        public int ExerciseHours { get; set; }

        [JsonPropertyName("weeklyHours")]
        public int WeeklyHours { get; set; }
        
        [JsonPropertyName("individualStudyHours")]
        public int IndividualStudyHours { get; set; }
    }
}
