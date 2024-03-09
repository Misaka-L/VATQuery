using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VATQuery.Application.Models.Vatsim {
    public class VatsimData {
        [JsonPropertyName("general")]
        public DataGeneralInfo Info { get; set; }
        [JsonPropertyName("pilots")]
        public VatsimPilot[] Pilots { get; set; }
        [JsonPropertyName("controllers")]
        public VatsimController[] Controllers { get; set; }
        [JsonPropertyName("atis")]
        public VatsimAtis[] Atis { get; set; }
        [JsonPropertyName("servers")]
        public VatsimServer[] Servers { get; set; }
        [JsonPropertyName("prefiles")]
        public VatsimPreFlight[] PreFlies { get; set; }
        [JsonPropertyName("facilities")]
        public Facility[] Facilities { get; set; }
        [JsonPropertyName("ratings")]
        public Rating[] Ratings { get; set; }
        [JsonPropertyName("pilot_ratings")]
        public PilotRatings[] PilotRatings { get; set; }

        public class DataGeneralInfo {
            [JsonPropertyName("version")]
            public int Version { get; set; }
            [JsonPropertyName("reload")]
            public int Reload { get; set; }
            [JsonPropertyName("update")]
            public string UpdateTimeStamp { get; set; }
            [JsonPropertyName("update_timestamp")]
            public DateTimeOffset UpdateTime { get; set; }
            [JsonPropertyName("connected_clients")]
            public int ConnectedClients { get; set; }
            [JsonPropertyName("unique_users")]
            public int UniqueUsers { get; set; }
        }

        public class VatsimServer {
            [JsonPropertyName("ident")]
            public string Ident { get; set; }
            [JsonPropertyName("hostname_or_ip")]
            public string HostnameOrIp { get; set; }
            [JsonPropertyName("location")]
            public string Location { get; set; }
            [JsonPropertyName("name")]
            public string Name { get; set; }
            [JsonPropertyName("clients_connection_allowed")]
            public int AllowedClientsConnection { get; set; }
            [JsonPropertyName("client_connections_allowed")]
            public bool MaxConnectionsCount { get; set; }
            [JsonPropertyName("is_sweatbox")]
            public bool IsSweatbox { get; set; }
        }
    }
}
