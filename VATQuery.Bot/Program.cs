using KookBotCraft.BotMarket.Extensions;
using KookBotCraft.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VATQuery.Application.Options;
using VATQuery.Application.Services;

var host = Host.CreateDefaultBuilder(args)
    .AddBotCraft()
    .AddBotMarket()
    .ConfigureServices(ConfigureServices)
    .Build();

await host.RunAsync();

void ConfigureServices(HostBuilderContext context, IServiceCollection services) {
    services.AddOptions<AMapOption>().BindConfiguration("AMap");

    services
        .AddSingleton<VatsimService>()
        .AddTransient<AMapWebApiService>()
        .AddHostedService<VatsimDataFetchHostService>();
}
