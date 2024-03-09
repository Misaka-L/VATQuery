using KookBotCraft.BotMarket.Options;
using KookBotCraft.BotMarket.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KookBotCraft.BotMarket.Extensions {
    public static class HostExtension {
        public static IHostBuilder AddBotMarket(this IHostBuilder builder) {
            return builder.ConfigureServices((services) => {
                services.AddOptions<BotMarketOption>().BindConfiguration("BotMarket");
                services
                    .AddHostedService<BotMarketUpdateHostService>();
            });
        }
    }
}
