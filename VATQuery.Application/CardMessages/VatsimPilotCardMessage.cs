using Kook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VATQuery.Application.Models.Vatsim;

namespace VATQuery.Application.CardMessages {
    public class VatsimPilotCardMessage {
        public VatsimPilot Pilot;
        public string MapUrl;

        public VatsimPilotCardMessage(VatsimPilot pilot, string mapUrl = "") {
            Pilot = pilot;
            MapUrl = mapUrl;
        }

        public Card[] Build() {
            return new Card[] {
                // 飞行员信息、航空器位置、飞行计划
                new CardBuilder()
                    .WithTheme(CardTheme.Success).WithSize(CardSize.Large)
                    // 飞行员信息
                    .AddSmallDescrption($"{Pilot.Name} · {Pilot.CID} · 连接服务器 {Pilot.Server}")
                    // Callsign
                    .AddHeader(Pilot.Callsign)
                    // Position
                    //.AddModule(new ContainerModuleBuilder().AddElement(new ImageElementBuilder().WithSource(MapUrl)))
                    // UpdateTime & LogonTime
                    .AddSmallTitle($"更新于 UTC {Pilot.LastUpdated:u} · 登录于 UTC {Pilot.LogonTime:u}")
                    .AddDivider()
                    // Aircraft
                    .AddSmallTitle("机型")
                    .AddModule(new SectionModuleBuilder().WithText(new ParagraphStructBuilder()
                        .WithColumnCount(3)
                        .AddParagraphStructContentField("机型", Pilot.FlightPlan.AircraftShort)
                        .AddParagraphStructContentField("全名", Pilot.FlightPlan.Aircraft)
                        .AddParagraphStructContentField("FAA", Pilot.FlightPlan.AircraftFaa)))
                    // FlightPlan
                    .AddSmallTitle("飞行计划")
                    .AddModule(new SectionModuleBuilder().WithText(new ParagraphStructBuilder()
                        .WithColumnCount(3)
                        .AddParagraphStructContentField("离场", Pilot.FlightPlan.Departure)
                        .AddParagraphStructContentField("落地", Pilot.FlightPlan.Arrival)
                        .AddParagraphStructContentField("备降", Pilot.FlightPlan.Alternate)
                        .AddParagraphStructContentField("计划高度", Pilot.FlightPlan.Altitude)
                        .AddParagraphStructContentField("计划起飞", Pilot.FlightPlan.DepartureTime)
                        .AddParagraphStructContentField("计划巡航时长", Pilot.FlightPlan.Enroutetime)
                        .AddParagraphStructContentField("计划燃油时长", Pilot.FlightPlan.FuelTime)
                        .AddParagraphStructContentField("分配应答机（非真实应答机）", Pilot.FlightPlan.AssignedTransponder)))
                    .AddSmallTitle("航路")
                    .AddModule(new SectionModuleBuilder().WithText(new KMarkdownElementBuilder().WithContent($"`{Pilot.FlightPlan.Route}`")))
                    .Build(),
                new CardBuilder()
                    .WithTheme(CardTheme.Warning).WithSize(CardSize.Large)
                    .AddSmallTitle("飞行数据")
                    .AddModule(new SectionModuleBuilder().WithText(new ParagraphStructBuilder()
                        .WithColumnCount(3)
                        .AddParagraphStructContentField("高度", Pilot.Altitude.ToString() + "ft")
                        .AddParagraphStructContentField("航向", Pilot.Heading.ToString())
                        .AddParagraphStructContentField("地速", Pilot.Groundspeed.ToString() + "kt")
                        .AddParagraphStructContentField("计划巡航空速", Pilot.FlightPlan.CruiseTas.ToString() + "kt")))
                    .Build(),
                new CardBuilder()
                    .WithTheme(CardTheme.Danger).WithSize(CardSize.Large)
                    .AddSmallTitle("追踪数据")
                    .AddModule(new SectionModuleBuilder().WithText(new ParagraphStructBuilder()
                        .WithColumnCount(3)
                        .AddParagraphStructContentField("经度", Pilot.Longitude.ToString())
                        .AddParagraphStructContentField("纬度", Pilot.Latitude.ToString())
                        .AddParagraphStructContentField("应答机", Pilot.Transponder)))
                    .Build()
            };
        }
    }
}
