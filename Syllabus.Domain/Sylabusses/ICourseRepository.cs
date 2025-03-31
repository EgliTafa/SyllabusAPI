namespace Syllabus.Domain.Sylabusses
{
    public interface ICourseRepository
    {
        ValueTask<Course?> GetByIdAsync(int id);
        ValueTask<List<Course>> GetAllAsync();
        ValueTask AddAsync(Course course);
        void Remove(Course course);
    }
}
