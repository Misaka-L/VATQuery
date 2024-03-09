using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using VATQuery.Application.Models.AMap;
using VATQuery.Application.Options;

namespace VATQuery.Application.Services {
    public class AMapWebApiService {
        private readonly ILogger<AMapWebApiService> _logger;
        private readonly IOptions<AMapOption> _amapOption;

        private readonly HttpClient _httpClient = new HttpClient();
        private const string _baseUrl = "https://restapi.amap.com";

        public AMapWebApiService(ILogger<AMapWebApiService> logger, IOptions<AMapOption> amapOption) {
            _logger = logger;
            _amapOption = amapOption;
        }

        public async ValueTask<Stream> GetMapImageAsync(
            float longitude,
            float latitude,
            int zoom = 6,
            string size = "440*180",
            AMapLabel[]? labels = null,
            AMapMarker[]? markers = null) {

            var query = new Dictionary<string, string>() {
                { "location", $"{longitude},{latitude}" },
                { "zoom", zoom.ToString() },
                { "size", size },
                { "key", _amapOption.Value.ApiKey }
            };

            if (labels is AMapLabel[] tempLables)
                query.Add("labels", tempLables.ToAMapMarkerFormte());
            if (markers is AMapMarker[] tempMarker)
                query.Add("markers", tempMarker.ToAMapMarkerFormte());

            var uri = QueryHelpers.AddQueryString(_baseUrl + "/v3/staticmap", query);
            return await _httpClient.GetStreamAsync(uri);
        }
    }

    public static class AMapMarkerArrayExtension {
        public static string ToAMapMarkerFormte(this AMapLabel[] labels) {
            var text = "";
            for (int index = 0; index != labels.Length; index++) {
                text += labels[index].ToString();

                if (index != labels.Length - 1)
                    text += "|";
            }

            return text;
        }

        public static string ToAMapMarkerFormte(this AMapMarker[] markers) {
            var text = "";
            for (int index = 0; index != markers.Length; index++) {
                text += markers[index].ToString();

                if (index != markers.Length - 1)
                    text += "|";
            }

            return text;
        }
    }
}
