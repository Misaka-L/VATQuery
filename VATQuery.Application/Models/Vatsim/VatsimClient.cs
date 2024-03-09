using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VATQuery.Application.Models.Vatsim {
    public class VatsimClient {
        [JsonPropertyName("cid")]
        public long CID { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("callsign")]
        public string Callsign { get; set; }
        [JsonPropertyName("last_updated")]
        public DateTimeOffset LastUpdated { get; set; }
    }
}
