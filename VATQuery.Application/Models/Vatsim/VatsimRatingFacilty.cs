using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VATQuery.Application.Models.Vatsim {
    public class VatsimRatingFacilty {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("_short")]
        public string ShortName { get; set; }
        [JsonPropertyName("_long")]
        public string LongName { get; set; }
    }

    public class Facility : VatsimRatingFacilty { }

    public class Rating : VatsimRatingFacilty { }

    public class PilotRatings : VatsimRatingFacilty { }
}
