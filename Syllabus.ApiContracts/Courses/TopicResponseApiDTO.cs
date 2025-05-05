namespace Syllabus.ApiContracts.Courses
{
    /// <summary>
    /// Response model representing a topic in a course.
    /// </summary>
    public class TopicResponseApiDTO
    {
        /// <summary>
        /// The title of the topic.
        /// </summary>
        public string Title { get; set; } = default!;

        /// <summary>
        /// The number of hours allocated to this topic.
        /// </summary>
        public int Hours { get; set; }

        /// <summary>
        /// Optional reference material for this topic.
        /// </summary>
        public string? Reference { get; set; }
    }
} 