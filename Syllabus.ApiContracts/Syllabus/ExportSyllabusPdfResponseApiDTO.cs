namespace Syllabus.ApiContracts.Syllabus
{
    public class ExportSyllabusPdfResponseApiDTO
    {
        public string FileName { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public byte[] FileBytes { get; set; } = default!;
    }
}
