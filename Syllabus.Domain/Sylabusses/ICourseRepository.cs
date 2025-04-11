namespace Syllabus.Domain.Sylabusses
{
    public interface ICourseRepository
    {
        ValueTask<Course?> GetByIdAsync(int id);
        ValueTask<List<Course>> GetAllAsync();
        ValueTask AddAsync(Course course);
        void Remove(Course course);
        ValueTask<CourseDetail?> GetDetailByCourseIdAsync(int courseId);
        ValueTask AddDetailAsync(CourseDetail detail);
        void RemoveDetail(CourseDetail detail);
        ValueTask<List<Topic>> GetTopicsByCourseIdAsync(int courseId);
        ValueTask AddTopicAsync(Topic topic);
        void RemoveTopic(Topic topic);
        ValueTask SaveChangesAsync();
    }
}
