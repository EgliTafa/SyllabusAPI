using Microsoft.EntityFrameworkCore;
using Syllabus.Domain.Sylabusses;
using Syllabus.Infrastructure.Data;

namespace Syllabus.Infrastructure.Repositories
{
    public class ProgramRepository : IProgramRepository
    {
        private readonly SyllabusDbContext _context;

        public ProgramRepository(SyllabusDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Program>> GetAllAsync()
        {
            return await _context.Programs
                .Include(p => p.Department)
                .Include(p => p.Syllabuses)
                .ToListAsync();
        }

        public async Task<List<Program>> GetByDepartmentIdAsync(int departmentId)
        {
            return await _context.Programs
                .Include(p => p.Department)
                .Include(p => p.Syllabuses)
                .Where(p => p.DepartmentId == departmentId)
                .ToListAsync();
        }

        public async Task<Program?> GetByIdAsync(int id)
        {
            return await _context.Programs
                .Include(p => p.Department)
                .Include(p => p.Syllabuses)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Program?> GetByNameAsync(string name)
        {
            return await _context.Programs
                .Include(p => p.Department)
                .Include(p => p.Syllabuses)
                .FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<Program?> GetByNameAndAcademicYearAsync(string name, string academicYear)
        {
            return await _context.Programs
                .Include(p => p.Department)
                .Include(p => p.Syllabuses)
                .FirstOrDefaultAsync(p => p.Name == name && p.AcademicYear == academicYear);
        }

        public async Task<Program> AddAsync(Program program)
        {
            await _context.Programs.AddAsync(program);
            return program;
        }

        public async Task<Program> UpdateAsync(Program program)
        {
            _context.Programs.Update(program);
            return program;
        }

        public async Task DeleteAsync(Program program)
        {
            _context.Programs.Remove(program);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
} 