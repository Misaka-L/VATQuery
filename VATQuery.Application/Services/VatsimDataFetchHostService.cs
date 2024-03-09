using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace VATQuery.Application.Services {
    public class VatsimDataFetchHostService : IHostedService {
        private readonly VatsimService _vatsimService;
        private readonly ILogger<VatsimDataFetchHostService> _logger;

        public VatsimDataFetchHostService(VatsimService vatsimService, ILogger<VatsimDataFetchHostService> logger) {
            _vatsimService = vatsimService;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            while (!cancellationToken.IsCancellationRequested) {
                _logger.LogInformation("正在更新 VATSIM 数据");
                try {
                    var data = await _vatsimService.GetVatsimDataAsync();
                    _logger.LogInformation("VATSIM 数据更新成功: {}", JsonSerializer.Serialize(data.Info));
                } catch (Exception ex) {
                    _logger.LogError(ex, "VATSIM 数据更新失败");
                }

                try {
                    await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
                } catch { }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            return Task.CompletedTask;
        }
    }
}
