using NLog;

namespace EpamAutomationTests.Core
{
    public static class Logger
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public static void Info(string message) => _logger.Info(message);
        public static void Error(string message) => _logger.Error(message);
        public static void Warn(string message) => _logger.Warn(message);
    }
}