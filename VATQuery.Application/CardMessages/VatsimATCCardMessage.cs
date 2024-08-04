using Kook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VATQuery.Application.Models.Vatsim;

namespace VATQuery.Application.CardMessages {
    public class VatsimATCCardMessage {
        public VatsimController ATC { get; set; }

        public VatsimATCCardMessage(VatsimController atc) {
            ATC = atc;
        }

        public Card[] Build() {
            var builder = new CardBuilder()
                .WithTheme(CardTheme.Success).WithSize(CardSize.Large)
                .AddSmallDescrption($"{ATC.Name} · {ATC.CID}")
                .AddHeader(ATC.Callsign)
                .AddSmallTitle($"更新于 UTC {ATC.LastUpdated:u} · 登录于 UTC {ATC.LogonTime:u} · 连接服务器 {ATC.Server}")
                .AddDivider()
                .AddSmallTitle("频率")
                .AddHeader(ATC.Frequency)
                .AddSmallTitle("ATIS");

            if (ATC is VatsimAtis atis) {
                builder.AddHeader($"CODE: {atis.AtisCode}");
            }

            var atisText = "";
            foreach (var line in ATC.AtisTexts) {
                atisText += line + "\n";
            }

            builder.AddModule(new SectionModuleBuilder().WithText(new KMarkdownElementBuilder().WithContent($"```\n{atisText}```")));

            return new Card[] {
                builder.Build()
            };
        }
    }
}
