using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Syllabus.Domain.Sylabusses
{
    [Owned]
    public class EvaluationBreakdown
    {
        [JsonPropertyName("participationPercent")]
        public int ParticipationPercent { get; set; }
        
        [JsonPropertyName("test1Percent")]
        public int Test1Percent { get; set; }
        
        [JsonPropertyName("test2Percent")]
        public int Test2Percent { get; set; }
        
        [JsonPropertyName("test3Percent")]
        public int Test3Percent { get; set; }
        
        [JsonPropertyName("finalExamPercent")]
        public int FinalExamPercent { get; set; }
    }

}
