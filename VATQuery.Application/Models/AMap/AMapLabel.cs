using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace VATQuery.Application.Models.AMap {
    public class AMapLabel {
        public string Content { get; set; } = "";
        public AMapFontFamily FontFamily { get; set; } = AMapFontFamily.MicrosoftYaHei;
        public bool IsBold { get; set; } = false;
        public int FontSize { get; set; } = 12;
        public string FontColor { get; set; } = "0x000000";
        public string BackgroundColor { get; set; } = "0xFFD800";
        public float Longitude;
        public float Latitude;

        public override string ToString() {
            return $"{Content},{(int)FontFamily},{Convert.ToInt32(IsBold)},{FontSize},{FontColor},{BackgroundColor}:{Longitude},{Latitude}";
        }
    }

    public enum AMapFontFamily {
        MicrosoftYaHei = 0,
        SimSun = 1,
        TimesNewRoman = 2,
        Helvetica = 3
    }
}
