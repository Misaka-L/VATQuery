using Kook.WebSocket;
using KookBotCraft.Core.Helper;
using KookBotCraft.Core.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KookBotCraft.Core.Services {
    public class KookClientHostService : IHostedService {
        private readonly KookSocketClient _socketClient;
        private readonly IOptions<KookOption> _kookOptions;
        private readonly ILogger<KookClientHostService> _logger;

        public KookClientHostService(KookSocketClient socketClient, ILogger<KookClientHostService> logger, IOptions<KookOption> kookOptions) {
            _socketClient = socketClient;
            _logger = logger;
            _kookOptions = kookOptions;
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            _socketClient.Log += _socketClient_Log;

            await _socketClient.LoginAsync(Kook.TokenType.Bot, _kookOptions.Value.Token);
            await _socketClient.StartAsync();
        }

        private Task _socketClient_Log(Kook.LogMessage message) {
            return KookLoggerHelper.HandleLog(_logger, message);
        }

        public async Task StopAsync(CancellationToken cancellationToken) {
            await _socketClient.StopAsync();
            await _socketClient.LogoutAsync();

            _socketClient.Log -= _socketClient_Log;
        }
    }
}
