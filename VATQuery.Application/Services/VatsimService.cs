using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VATQuery.Application.Models.Vatsim;

namespace VATQuery.Application.Services {
    public class VatsimService {
        private const string _statusUrl = "https://status.vatsim.net/status.json";
        private const string _envetUrl = "https://my.vatsim.net/api/v1/events/all";
        private const string _vatsimStatusUrl = "https://network-status.vatsim.net/api/v2/status.json";
        private const string _vatsimCountUrl = "https://stats.vatsim.net/user_count_json";
        private const string _vatsimMetarUrl = "https://metar.vatsim.net/";

        private VatsimStatus _status = new VatsimStatus();
        private DateTimeOffset? _lastUpdated = null;
        private VatsimData? _vatsimData;

        private readonly ILogger<VatsimService> _logger;
        private readonly HttpClient _httpClient = new HttpClient();

        public VatsimService(ILogger<VatsimService> logger) {
            _logger = logger;
        }

        public VatsimStatus GetStatus() {
            return _status;
        }

        public async ValueTask<VatsimStatus> GetStatusAsync() {
            try {
                if (await _httpClient.GetFromJsonAsync<VatsimStatus>(_statusUrl) is VatsimStatus status) {
                    _status = status;

                    return _status;
                }

                throw new InvalidDataException("VATSIM 服务器返回了错误的数据");
            } catch (Exception ex) {
                _logger.LogError(ex, "获取 VATSIM 数据 Urls 错误");
                throw;
            }
        }

        public VatsimData? GetVatsimData() {
            return _vatsimData;
        }

        public async ValueTask<VatsimData> GetVatsimDataAsync() {
            await checkStatusUpdate();

            try {
                if (await _httpClient.GetFromJsonAsync<VatsimData>(getVatsimUrl(_status.DataUrls.DataUrls)) is VatsimData data) {
                    _vatsimData = data;

                    return data;
                }

                throw new InvalidDataException("VATSIM 服务器返回了错误的数据");
            } catch (Exception ex) {
                _logger.LogError(ex, "获取 VATSIM 数据错误");
                throw;
            }
        }

        public async ValueTask<string> GetMetarAsync(string icao) {
            await checkStatusUpdate();

            try {
                return await _httpClient.GetStringAsync(_vatsimMetarUrl + icao);
            } catch (Exception ex) {
                _logger.LogError(ex, "获取 VATSIM METAR 数据错误");
                throw;
            }
        }

        public async ValueTask<VatsimEventApiResponse> GetVatsimEventsAsync() {
            try {
                if (await _httpClient.GetFromJsonAsync<VatsimEventApiResponse>(_envetUrl) is VatsimEventApiResponse data)
                    return data;

                throw new InvalidDataException("VATSIM 服务器返回了错误的数据");
            } catch (Exception ex) {
                _logger.LogError(ex, "获取 VATSIM 活动数据错误");
                throw;
            }
        }

        private string getVatsimUrl(string[] urls) {
            return urls[0];
        }

        private async Task checkStatusUpdate() {
            if (_lastUpdated is DateTimeOffset time && DateTimeOffset.Now - time < TimeSpan.FromMinutes(15))
                return;

            await GetStatusAsync();
        }
    }
}
