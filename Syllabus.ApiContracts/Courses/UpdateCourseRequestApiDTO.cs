using Syllabus.Domain.Sylabusses;
using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Courses
{
    /// <summary>
    /// Request model for updating an existing course.
    /// </summary>
    public class UpdateCourseRequestApiDTO
    {
        /// <summary>
        /// The ID of the course to update.
        /// </summary>
        [JsonPropertyName("courseId")]
        public int CourseId { get; set; }

        /// <summary>
        /// The updated title of the course.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = default!;

        /// <summary>
        /// The updated code of the course.
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = default!;

        /// <summary>
        /// The semester in which the course is taught.
        /// </summary>
        [JsonPropertyName("semester")]
        public int Semester { get; set; }

        /// <summary>
        /// Updated number of lecture hours.
        /// </summary>
        [JsonPropertyName("lectureHours")]
        public int LectureHours { get; set; }

        /// <summary>
        /// Updated number of seminar hours.
        /// </summary>
        [JsonPropertyName("seminarHours")]
        public int SeminarHours { get; set; }

        /// <summary>
        /// Updated number of lab hours.
        /// </summary>
        [JsonPropertyName("labHours")]
        public int LabHours { get; set; }

        /// <summary>
        /// Updated number of credits for the course.
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
        /// The academic year (1, 2, or 3) in which the course is taught.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// The elective group for this course ('Elective I', 'Elective II', or null).
        /// </summary>
        public string? ElectiveGroup { get; set; }
    }
}
