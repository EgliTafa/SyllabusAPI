namespace Syllabus.ApiContracts.Syllabus
{
    public class ListAllSyllabusesResponseApiDTO
    {
        public List<SyllabusResponseApiDTO> Syllabuses { get; set; } = new List<SyllabusResponseApiDTO>();
    }
}
