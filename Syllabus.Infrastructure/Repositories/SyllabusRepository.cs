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
                .Include(s => s.Program)
                    .ThenInclude(p => p.Department)
                .Include(s => s.Courses)
                    .ThenInclude(c => c.Detail)
                        .ThenInclude(d => d!.Topics)
                .ToListAsync();
        }

        public async ValueTask<Sylabus?> GetByIdAsync(int id)
        {
            return await _context.Syllabuses
                .Include(s => s.Program)
                    .ThenInclude(p => p.Department)
                .Include(s => s.Courses)
                    .ThenInclude(c => c.Detail)
                        .ThenInclude(d => d!.Topics)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public void Remove(Sylabus syllabus)
        {
            _context.Syllabuses.Remove(syllabus);
        }
        public async ValueTask SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
