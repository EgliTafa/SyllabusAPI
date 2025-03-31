using Microsoft.EntityFrameworkCore;
using Syllabus.Domain.Sylabusses;
using Syllabus.Infrastructure.Data;

namespace Syllabus.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SyllabusDbContext _context;

        public CourseRepository(SyllabusDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async ValueTask AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
        }

        public async ValueTask<List<Course>> GetAllAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async ValueTask<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public void Remove(Course course)
        {
            _context.Courses.Remove(course);
        }
    }
}
