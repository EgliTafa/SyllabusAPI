using Syllabus.Domain.Sylabusses;

namespace Syllabus.Domain.Sylabusses
{
    public interface IProgramRepository
    {
        Task<List<Program>> GetAllAsync();
        Task<List<Program>> GetByDepartmentIdAsync(int departmentId);
        Task<Program?> GetByIdAsync(int id);
        Task<Program?> GetByNameAsync(string name);
        Task<Program> AddAsync(Program program);
        Task<Program> UpdateAsync(Program program);
        Task DeleteAsync(Program program);
        Task SaveChangesAsync();
    }
} 