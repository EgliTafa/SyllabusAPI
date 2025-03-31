using Microsoft.EntityFrameworkCore;
using Syllabus.Domain.Sylabusses;
using Syllabus.Infrastructure.Data;

namespace Syllabus.Infrastructure.Repositories
{
    public class SyllabusRepository : ISyllabusRepository
    {
        private readonly SyllabusDbContext _context;

        public SyllabusRepository(SyllabusDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async ValueTask AddAsync(Sylabus syllabus)
        {
            await _context.Syllabuses.AddAsync(syllabus);
        }

        public async ValueTask<List<Sylabus>> GetAllAsync()
        {
            return await _context.Syllabuses
                .Include(s => s.Courses)
                .ToListAsync();
        }

        public async ValueTask<Sylabus?> GetByIdAsync(int id)
        {
            return await _context.Syllabuses
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public void Remove(Sylabus syllabus)
        {
            _context.Syllabuses.Remove(syllabus);
        }
    }
}
