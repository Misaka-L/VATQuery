using Kook;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KookBotCraft.Core.Helper {
    internal class KookLoggerHelper {
        public static Task HandleLog(ILogger logger, LogMessage message) {
            var severity = message.Severity switch {
                LogSeverity.Critical => LogLevel.Critical,
                LogSeverity.Error => LogLevel.Error,
                LogSeverity.Warning => LogLevel.Warning,
                LogSeverity.Info => LogLevel.Information,
                LogSeverity.Verbose => LogLevel.Trace,
                LogSeverity.Debug => LogLevel.Debug,
                _ => LogLevel.Information
            };

            if (message.Exception != null) {
                logger.Log(severity, message.Exception, $"[{message.Source}] {message.Message}");
            } else {
                logger.Log(severity, message.Message, $"[{message.Source}] {message.Message}");
            }

            return Task.CompletedTask;
        }
    }
}
