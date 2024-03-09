using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VATQuery.Application.Models.Vatsim {
    public class VatsimPilot : VatsimPreFlight, IVatsimOnlineCient {
        [JsonPropertyName("pilot_rating")]
        public int PilotRating { get; set; }
        [JsonPropertyName("latitude")]
        public float Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public float Longitude { get; set; }
        [JsonPropertyName("altitude")]
        public long Altitude { get; set; }
        [JsonPropertyName("groundspeed")]
        public int Groundspeed { get; set; }
        [JsonPropertyName("transponder")]
        public string? Transponder { get; set; }
        [JsonPropertyName("heading")]
        public int Heading { get; set; }
        [JsonPropertyName("qnh_i_hg")]
        public float QnhHg { get; set; }
        [JsonPropertyName("qnh_mb")]
        public int Qnh { get; set; }
        [JsonPropertyName("logon_time")]
        public DateTimeOffset? LogonTime { get; set; }
        [JsonPropertyName("server")]
        public string? Server { get; set; }
    }

    public class VatsimPreFlight : VatsimClient {
        [JsonPropertyName("flight_plan")]
        public VatsimFlightPlanData FlightPlan { get; set; } = new VatsimFlightPlanData();
    }
}
