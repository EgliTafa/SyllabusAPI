using System.Text.Json.Serialization;

namespace Syllabus.Domain.Sylabusses
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CourseType
    {
        Mandatory, // B - Bazë
        Advanced, // A - Avancuar
        Specialized, // C - Specializim
        Elective, // D - Zgjedhje
        FinalProject // E - Projekt Final
    }
}