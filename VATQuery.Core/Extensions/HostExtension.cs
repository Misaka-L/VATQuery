using Kook.Commands;
using Kook.WebSocket;
using KookBotCraft.Core.Options;
using KookBotCraft.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KookBotCraft.Core.Extensions {
    public static class HostExtension {
        public static IHostBuilder AddBotCraft(this IHostBuilder hostBuilder) {
            return hostBuilder
                .UseContentRoot(AppContext.BaseDirectory)
                .ConfigureAppConfiguration(builder => {
                    builder
#if DEBUG
                        .AddJsonFile("appsettings.development.json")
#endif
                        .AddJsonFile("appsettings.json")
                        .AddEnvironmentVariables();
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
