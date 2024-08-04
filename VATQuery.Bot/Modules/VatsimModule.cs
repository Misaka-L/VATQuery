using Kook;
using Kook.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VATQuery.Application.CardMessages;
using VATQuery.Application.Models.AMap;
using VATQuery.Application.Models.Vatsim;
using VATQuery.Application.Services;

namespace VATQuery.Bot.Modules {
    [Group("vatq")]
    public class VatsimModule : ModuleBase<SocketCommandContext> {
        private readonly VatsimService _vatsimService;

        public VatsimModule(VatsimService vatsimService) {
            _vatsimService = vatsimService;
        }

        [Command("metar")]
        public async Task GetMetar(string icao) {
            var result = await _vatsimService.GetMetarAsync(icao);
            await ReplyCardAsync(
                new CardBuilder()
                    .WithTheme(CardTheme.None).WithSize(CardSize.Large)
                    .AddModule(new HeaderModuleBuilder().WithText(new PlainTextElementBuilder().WithContent($"{icao} 的 METAR 报告为")))
                    .AddModule(new SectionModuleBuilder().WithText(new KMarkdownElementBuilder().WithContent($"`{result}`")))
                    .Build()
                    , isQuote: true);
        }

        [Group("atis")]
        public class VatsimAtisModule : ModuleBase<SocketCommandContext> {
            private readonly VatsimService _vatsimService;

            public VatsimAtisModule(VatsimService vatsimService) {
                _vatsimService = vatsimService;
            }

            [Command]
            [Alias("cs", "callsign")]
            [Priority(-1)]
            public async Task GetAtc(string callsign) {
                var data = _vatsimService.GetVatsimData() is VatsimData vatsimData ? vatsimData : await _vatsimService.GetVatsimDataAsync();

                var result = data.Atis.Where(atis => atis.Callsign.Contains(callsign));
                if (result.Count() != 0) {
                    await ReplyCardsAsync(new VatsimATCCardMessage(result.First()).Build());
                } else {
                    await ReplyTextAsync("Atis 不存在");
                }
            }

            [Command("cid")]
            public async Task GetAtcs(long cid) {
                var data = _vatsimService.GetVatsimData() is VatsimData vatsimData ? vatsimData : await _vatsimService.GetVatsimDataAsync();

                var result = data.Atis.Where(atis => atis.CID == cid);
                if (result.Count() != 0) {
                    await ReplyCardsAsync(new VatsimATCCardMessage(result.First()).Build());
                } else {
                    await ReplyTextAsync("Atis 不存在");
                }
            }
        }

        [Group("atc")]
        public class VatsimATCModule : ModuleBase<SocketCommandContext> {
            private readonly VatsimService _vatsimService;

            public VatsimATCModule(VatsimService vatsimService) {
                _vatsimService = vatsimService;
            }

            [Command]
            [Alias("cs", "callsign")]
            [Priority(-1)]
            public async Task GetAtc(string callsign) {
                var data = _vatsimService.GetVatsimData() is VatsimData vatsimData ? vatsimData : await _vatsimService.GetVatsimDataAsync();

                var result = data.Controllers.Where(controller => controller.Callsign.Contains(callsign));
                if (result.Count() != 0) {
                    await ReplyCardsAsync(new VatsimATCCardMessage(result.First()).Build());
                } else {
                    await ReplyTextAsync("ATC 不存在");
                }
            }

            [Command("cid")]
            public async Task GetAtcs(long cid) {
                var data = _vatsimService.GetVatsimData() is VatsimData vatsimData ? vatsimData : await _vatsimService.GetVatsimDataAsync();

                var result = data.Controllers.Where(controller => controller.CID == cid);
                if (result.Count() != 0) {
                    await ReplyCardsAsync(new VatsimATCCardMessage(result.First()).Build());
                } else {
                    await ReplyTextAsync("ATC 不存在");
                }
            }
        }

        [Group("flight")]
        public class VatsimFlightModule : ModuleBase<SocketCommandContext> {
            private readonly VatsimService _vatsimService;
            private readonly AMapWebApiService _amapWebApiService;

            public VatsimFlightModule(VatsimService vatsimService, AMapWebApiService amapWebApiService) {
                _vatsimService = vatsimService;
                _amapWebApiService = amapWebApiService;
            }

            [Command]
            [Alias("cs", "callsign")]
            [Priority(-1)]
            public async Task GetFlightByCallsign(string callsign) {
                var data = _vatsimService.GetVatsimData() is VatsimData vatsimData ? vatsimData : await _vatsimService.GetVatsimDataAsync();

                if (data.Pilots.Any(pilot => pilot.Callsign.Contains(callsign))) {
                    var pilot = data.Pilots.Where(pilot => pilot.Callsign.Contains(callsign)).First();
                    //var mapStream = await _amapWebApiService.GetMapImageAsync(pilot.Longitude, pilot.Latitude, labels: new AMapLabel[] {
                    //    new AMapLabel { Longitude = pilot.Longitude, Latitude = pilot.Latitude, Content = pilot.Callsign }
                    //}, markers: new AMapMarker[] {
                    //    new AMapMarker{ Longitude = pilot.Longitude, Latitude = pilot.Latitude }
                    //});

                    //var mapUrl = await Context.Client.Rest.CreateAssetAsync(mapStream, "map.png");
                    await ReplyCardsAsync(new VatsimPilotCardMessage(pilot).Build(), true);
                } else {
                    await ReplyTextAsync("航班不存在");
                }
            }

            [Command("cid")]
            public async Task GetFlightByCid(long cid) {
                var data = _vatsimService.GetVatsimData() is VatsimData vatsimData ? vatsimData : await _vatsimService.GetVatsimDataAsync();

                if (data.Pilots.Any(pilot => pilot.CID == cid)) {
                    var pilot = data.Pilots.Where(pilot => pilot.CID == cid).First();
                    //var mapStream = await _amapWebApiService.GetMapImageAsync(pilot.Longitude, pilot.Latitude, labels: new AMapLabel[] {
                    //    new AMapLabel { Longitude = pilot.Longitude, Latitude = pilot.Latitude, Content = pilot.Callsign }
                    //}, markers: new AMapMarker[] {
                    //    new AMapMarker{ Longitude = pilot.Longitude, Latitude = pilot.Latitude }
                    //});

                    //var mapUrl = await Context.Client.Rest.CreateAssetAsync(mapStream, "map.png");
                    await ReplyCardsAsync(new VatsimPilotCardMessage(pilot).Build(), true);
                } else {
                    await ReplyTextAsync("航班不存在");
                }
            }

            [Command("name")]
            public async Task GetFlightByName(string name) {
                var data = _vatsimService.GetVatsimData() is VatsimData vatsimData ? vatsimData : await _vatsimService.GetVatsimDataAsync();

                if (data.Pilots.Any(pilot => pilot.Name.Contains(name))) {
                    var pilot = data.Pilots.Where(pilot => pilot.Name.Contains(name)).First();
                    //var mapStream = await _amapWebApiService.GetMapImageAsync(pilot.Longitude, pilot.Latitude, labels: new AMapLabel[] {
                    //    new AMapLabel { Longitude = pilot.Longitude, Latitude = pilot.Latitude, Content = pilot.Callsign }
                    //}, markers: new AMapMarker[] {
                    //    new AMapMarker{ Longitude = pilot.Longitude, Latitude = pilot.Latitude }
                    //});

                    //var mapUrl = await Context.Client.Rest.CreateAssetAsync(mapStream, "map.png");
                    await ReplyCardsAsync(new VatsimPilotCardMessage(pilot).Build(), true);
                } else {
                    await ReplyTextAsync("航班不存在");
                }
            }
        }
    }
}
