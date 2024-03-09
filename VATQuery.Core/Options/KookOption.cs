using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KookBotCraft.Core.Options {
    public class KookOption {
        public string Token { get; set; }
        public string CommandPrefix { get; set; } = ".";
        public bool RequierMention { get; set; } = false;
    }
}
