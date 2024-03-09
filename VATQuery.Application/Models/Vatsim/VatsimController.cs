using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VATQuery.Application.Models.Vatsim {
    public class VatsimController : VatsimClient, IVatsimOnlineCient {
        public string Frequency { get; set; }
        [JsonPropertyName("facility")]
        public int Facility { get; set; }
        [JsonPropertyName("rating")]
        public int Rating { get; set; }
        [JsonPropertyName("visual_range")]
        public int VisualRange { get; set; }
        [JsonPropertyName("text_atis")]
        public string[] AtisTexts { get; set; }
        [JsonPropertyName("logon_time")]
        public DateTimeOffset? LogonTime { get; set; }
        [JsonPropertyName("server")]
        public string? Server { get; set; }
    }

    public class VatsimAtis : VatsimController {
        [JsonPropertyName("atis_code")]
        public string AtisCode { get; set; }
    }
}
