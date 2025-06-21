using Syllabus.Domain.Sylabusses;
using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Courses
{
    /// <summary>
    /// Request model for creating a new course.
    /// </summary>
    public class CreateCourseRequestApiDTO
    {
        /// <summary>
        /// The title of the course.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = default!;

        /// <summary>
        /// The code of the course (e.g., CS101).
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = default!;

        /// <summary>
        /// The semester in which the course is taught.
        /// </summary>
        [JsonPropertyName("semester")]
        public int Semester { get; set; }

        /// <summary>
        /// Number of lecture hours for the course.
        /// </summary>
        [JsonPropertyName("lectureHours")]
        public int LectureHours { get; set; }

        /// <summary>
        /// Number of seminar hours for the course.
        /// </summary>
        [JsonPropertyName("seminarHours")]
        public int SeminarHours { get; set; }

        /// <summary>
        /// Number of lab hours for the course.
        /// </summary>
        [JsonPropertyName("labHours")]
        public int LabHours { get; set; }

        /// <summary>
        /// Number of practice hours for the course.
        /// </summary>
        [JsonPropertyName("practiceHours")]
        public int PracticeHours { get; set; }

        /// <summary>
        /// Number of credits assigned to the course.
        /// </summary>
        [JsonPropertyName("credits")]
        public int Credits { get; set; }

        /// <summary>
        /// The evaluation method used for this course.
        /// </summary>
        [JsonPropertyName("evaluation")]
        public EvaluationMethod Evaluation { get; set; }

        /// <summary>
        /// The type of the course (e.g., Mandatory, Elective).
        /// </summary>
        [JsonPropertyName("type")]
        public CourseType Type { get; set; }

        /// <summary>
        /// The ID of the syllabus to which this course belongs.
        /// </summary>
        [JsonPropertyName("syllabusId")]
        public int SyllabusId { get; set; }

        /// <summary>
        /// Optional course details to be created along with the course.
        /// </summary>
        [JsonPropertyName("detail")]
        public CourseDetailRequestApiDTO? Detail { get; set; }

        /// <summary>
        /// The academic year (1, 2, or 3) in which the course is taught.
        /// </summary>
        [JsonPropertyName("year")]
        public int Year { get; set; }

        /// <summary>
        /// The elective group for this course ('Elective I', 'Elective II', or null).
        /// </summary>
        [JsonPropertyName("electiveGroup")]
        public string? ElectiveGroup { get; set; }
    }
}
