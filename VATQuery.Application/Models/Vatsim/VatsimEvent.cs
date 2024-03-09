using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static VATQuery.Application.Models.Vatsim.VatsimEvent;

namespace VATQuery.Application.Models.Vatsim {
    public class VatsimEventApiResponse {
        [JsonPropertyName("data")]
        public VatsimEvent[] data { get; set; }
    }

    public class VatsimEvent {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("vso_name")]
        public object vso_name { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public VatsimEventOrganiser[] organisers { get; set; }
        public VatsimEventAirport[] airports { get; set; }
        public VatsimEventRoute[] routes { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public string short_description { get; set; }
        public string description { get; set; }
        public string banner { get; set; }
    }

    public class VatsimEventOrganiser {
        public string region { get; set; }
        public string division { get; set; }
        public object subdivision { get; set; }
        public bool organised_by_vatsim { get; set; }
    }

    public class VatsimEventAirport {
        public string icao { get; set; }
    }

    public class VatsimEventRoute {
        public string departure { get; set; }
        public string arrival { get; set; }
        public string route { get; set; }
    }
}
