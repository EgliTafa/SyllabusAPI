namespace Syllabus.Domain.Sylabusses
{
    public class Topic
    {
        public int Id { get; set; }
        public int CourseDetailId { get; set; }
        public CourseDetail CourseDetail { get; set; } = null!;
        public string Title { get; set; } = default!;
        public int Hours { get; set; }
        public string? Reference { get; set; }
    }
}
