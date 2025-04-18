namespace Syllabus.Domain.Sylabusses
{
    public interface ISyllabusRepository
    {
        ValueTask<Sylabus?> GetByIdAsync(int id);
        ValueTask<List<Sylabus>> GetAllAsync();
        ValueTask AddAsync(Sylabus syllabus);
        void Remove(Sylabus syllabus);
        ValueTask SaveChangesAsync();
    }
}
