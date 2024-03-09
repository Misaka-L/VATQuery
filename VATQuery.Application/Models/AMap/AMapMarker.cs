using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VATQuery.Application.Models.AMap {
    public class AMapMarker {
        public AMapMarkerSize Size { get; set; } = AMapMarkerSize.Medium;
        public string Color { get; set; } = "0xFFD800";
        public string Label { get; set; } = "";
        public float Longitude;
        public float Latitude;

        public override string ToString() {
            return $"{Size.ToAMapFormate()},{Color},{Label}:{Longitude},{Latitude}";
        }
    }

    public enum AMapMarkerSize {
        Small,
        Medium,
        Large
    }

    public static class AMapMarkerSizeExtension {
        public static string ToAMapFormate(this AMapMarkerSize marker) {
            return marker switch {
                AMapMarkerSize.Small => "small",
                AMapMarkerSize.Medium => "mid",
                AMapMarkerSize.Large => "large",
                _ => marker.ToString()
            };
        }
    }
}
