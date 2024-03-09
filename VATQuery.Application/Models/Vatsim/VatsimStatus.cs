using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VATQuery.Application.Models.Vatsim {
    public class VatsimStatus {
        [JsonPropertyName("data")]
        public VatsimDataUrls DataUrls { get; set; } = new VatsimDataUrls();
        [JsonPropertyName("user")]
        public string[] UserDataUrls { get; set; } = new string[] { "https://stats.vatsim.net/search_id.php" };
        [JsonPropertyName("metar")]
        public string[] MetarDaturls { get; set; } = new string[] { "http://metar.vatsim.net/metar.php" };

        public class VatsimDataUrls {
            [JsonPropertyName("v3")]
            public string[] DataUrls { get; set; } = new string[] { "https://data.vatsim.net/v3/vatsim-data.json" };
            [JsonPropertyName("transceivers")]
            public string[] TransceiversDataUrls { get; set; } = new string[] { "https://data.vatsim.net/v3/transceivers-data.json" };
            [JsonPropertyName("servers")]
            public string[] ServersDataUrls { get; set; } = new string[] { "https://data.vatsim.net/v3/vatsim-servers.json" };
            [JsonPropertyName("servers_sweatbot")]
            public string[] ServersSweatboxDataUrls { get; set; } = new string[] { "https://data.vatsim.net/v3/sweatbox-servers.json" };
            [JsonPropertyName("servers_all")]
            public string[] AllServersDataUrls { get; set; } = new string[] { "https://data.vatsim.net/v3/all-servers.json" };
        }
    }
}
