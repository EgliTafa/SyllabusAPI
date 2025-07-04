using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Syllabus
{
    public class ExportSyllabusPdfRequestApiDTO
    {
        [JsonPropertyName("syllabusId")]
        public int SyllabusId { get; set; }
    }
}
