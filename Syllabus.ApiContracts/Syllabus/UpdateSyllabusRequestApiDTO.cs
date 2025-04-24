namespace Syllabus.ApiContracts.Syllabus
{
    public class UpdateSyllabusRequestApiDTO
    {
        public int SyllabusId { get; set; }
        public string Name { get; set; } = default!;
    }
}
