using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VATQuery.Application.Models.Vatsim {
    public class VatsimFlightPlanData {
        [JsonPropertyName("flight_rules")]
        public string FlightRules { get; set; }
        [JsonPropertyName("aircraft")]
        public string Aircraft { get; set; }
        [JsonPropertyName("aircraft_faa")]
        public string AircraftFaa { get; set; }
        [JsonPropertyName("aircraft_short")]
        public string AircraftShort { get; set; }
        [JsonPropertyName("departure")]
        public string Departure { get; set; }
        [JsonPropertyName("arrival")]
        public string Arrival { get; set; }
        [JsonPropertyName("alternate")]
        public string Alternate { get; set; }
        [JsonPropertyName("cruise_tas")]
        public string CruiseTas { get; set; }
        [JsonPropertyName("altitude")]
        public string Altitude { get; set; }
        [JsonPropertyName("deptime")]
        public string DepartureTime { get; set; }
        [JsonPropertyName("enroute_time")]
        public string Enroutetime { get; set; }
        [JsonPropertyName("fuel_time")]
        public string FuelTime { get; set; }
        [JsonPropertyName("remarks")]
        public string Remarks { get; set; }
        [JsonPropertyName("route")]
        public string Route { get; set; }
        [JsonPropertyName("revision_id")]
        public int RevisionId { get; set; }
        [JsonPropertyName("assigned_transponder")]
        public string AssignedTransponder { get; set; }
    }
}
