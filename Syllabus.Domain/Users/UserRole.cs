using System.Text.Json.Serialization;

namespace Syllabus.Domain.Users
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole
    {
        Student,
        Professor,
        Administrator
    }
}