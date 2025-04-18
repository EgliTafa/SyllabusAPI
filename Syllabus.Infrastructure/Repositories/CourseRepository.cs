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
            return await _context.Courses
                .Include(c => c.Detail!)
                    .ThenInclude(d => d.Topics)
                .ToListAsync();
        }

        public async ValueTask<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Detail!)
                    .ThenInclude(d => d.Topics)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Remove(Course course)
        {
            _context.Courses.Remove(course);
        }

        public async ValueTask<CourseDetail?> GetDetailByCourseIdAsync(int courseId)
        {
            return await _context.CourseDetails
                .Include(cd => cd.Topics)
                .FirstOrDefaultAsync(cd => cd.CourseId == courseId);
        }

        public async ValueTask AddDetailAsync(CourseDetail detail)
        {
            await _context.CourseDetails.AddAsync(detail);
        }

        public void RemoveDetail(CourseDetail detail)
        {
            _context.CourseDetails.Remove(detail);
        }

        public async ValueTask<List<Topic>> GetTopicsByCourseIdAsync(int courseId)
        {
            return await _context.CourseTopics
                .Where(t => t.CourseDetail.CourseId == courseId)
                .ToListAsync();
        }

        public async ValueTask AddTopicAsync(Topic topic)
        {
            await _context.CourseTopics.AddAsync(topic);
        }

        public void RemoveTopic(Topic topic)
        {
            _context.CourseTopics.Remove(topic);
        }
        public async ValueTask<List<Course>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.Courses
                .Include(c => c.Detail!)
                    .ThenInclude(d => d.Topics)
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();
        }


        public async ValueTask SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
