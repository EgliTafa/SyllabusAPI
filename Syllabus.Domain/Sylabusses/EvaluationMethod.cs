using System.Text.Json.Serialization;

namespace Syllabus.Domain.Sylabusses
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EvaluationMethod
    {
        Exam, // P - Provim
        ContinuousAssessment, // V - Vlerësim i vazhduar
        Pass, // F - Fiton
        DiplomaExam // E - Provim Diplome
    }
}