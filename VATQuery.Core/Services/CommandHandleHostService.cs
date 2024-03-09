using Kook.Commands;
using Kook.WebSocket;
using KookBotCraft.Core.Helper;
using KookBotCraft.Core.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KookBotCraft.Core.Services {
    public class CommandHandleHostService : IHostedService {
        private readonly KookSocketClient _socketClient;
        private readonly CommandService _commandService;

        private readonly ILogger<CommandHandleHostService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IOptions<KookOption> _kookOptions;

        public CommandHandleHostService(KookSocketClient socketClient,
                                        ILogger<CommandHandleHostService> logger,
                                        CommandService commandService,
                                        IServiceProvider serviceProvider,
                                        IOptions<KookOption> kookOptions) {
            _socketClient = socketClient;
            _logger = logger;
            _commandService = commandService;
            _serviceProvider = serviceProvider;
            _kookOptions = kookOptions;
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            _commandService.Log += _commandService_Log;
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), _serviceProvider);

            _socketClient.DirectMessageReceived += _socketClient_MessageReceived;
            _socketClient.MessageReceived += _socketClient_MessageReceived;
        }

        private Task _commandService_Log(Kook.LogMessage message) {
            return KookLoggerHelper.HandleLog(_logger, message);
        }

        private async Task _socketClient_MessageReceived(SocketMessage arg) {
            if (arg is SocketUserMessage message && !message.Author.IsBot.GetValueOrDefault(true)) {
                var argPos = 0;

                if (message.HasStringPrefix(_kookOptions.Value.CommandPrefix, ref argPos) &&
                    (!_kookOptions.Value.RequierMention | message.HasMentionPrefix(_socketClient.CurrentUser, ref argPos))) {

                    var context = new SocketCommandContext(_socketClient, message);
                    await _commandService.ExecuteAsync(context, argPos, _serviceProvider);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            _socketClient.DirectMessageReceived -= _socketClient_MessageReceived;
            _socketClient.MessageReceived -= _socketClient_MessageReceived;
            _commandService.Log -= _commandService_Log;

            return Task.CompletedTask;
        }
    }
}
