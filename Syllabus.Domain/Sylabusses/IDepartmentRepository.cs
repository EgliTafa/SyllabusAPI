using Syllabus.Domain.Sylabusses;

namespace Syllabus.Domain.Sylabusses
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task<Department?> GetByNameAsync(string name);
        Task<Department> AddAsync(Department department);
        Task<Department> UpdateAsync(Department department);
        Task DeleteAsync(Department department);
        Task SaveChangesAsync();
    }
} 