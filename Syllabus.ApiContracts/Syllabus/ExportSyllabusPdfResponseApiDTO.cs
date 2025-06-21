using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Syllabus
{
    public class ExportSyllabusPdfResponseApiDTO
    {
        [JsonPropertyName("fileName")]
        public string FileName { get; set; } = default!;
        
        [JsonPropertyName("contentType")]
        public string ContentType { get; set; } = default!;
        
        [JsonPropertyName("fileBytes")]
        public byte[] FileBytes { get; set; } = default!;
    }
}
