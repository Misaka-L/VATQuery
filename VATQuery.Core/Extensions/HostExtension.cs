using Kook.Commands;
using Kook.WebSocket;
using KookBotCraft.Core.Options;
using KookBotCraft.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace KookBotCraft.Core.Extensions {
    public static class HostExtension {
        public static IHostBuilder AddBotCraft(this IHostBuilder hostBuilder) {
            return hostBuilder
                .UseContentRoot(AppContext.BaseDirectory)
                .ConfigureAppConfiguration(builder =>
                {
#if DEBUG
                        builder.AddJsonFile("appsettings.development.json");
#endif
                })
                .ConfigureServices((services) => {
                    services.AddOptions<KookOption>().BindConfiguration("Kook");

                    services
                        .AddSingleton(_ => new KookSocketClient(new KookSocketConfig {
                            LogLevel = Kook.LogSeverity.Verbose
                        }))
                        .AddSingleton(_ => new CommandService(new CommandServiceConfig {
                            LogLevel = Kook.LogSeverity.Verbose,
                        }))
                        .AddHostedService<KookClientHostService>()
                        .AddHostedService<CommandHandleHostService>();
                })
                .ConfigureLogging((context, builder) => {
                    builder
                        .AddConfiguration(context.Configuration)
                        .SetMinimumLevel(LogLevel.Trace)
                        .AddSimpleConsole((config) => {
                            config.ColorBehavior = LoggerColorBehavior.Enabled;
                            config.IncludeScopes = true;
                        })
                        .AddDebug();
                });
        }
    }
}
