using NLog;
using System;

namespace EpamAutomationTests.Core
{
    public static class Logger
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        static Logger()
        {
            var config = new NLog.Config.LoggingConfiguration();
            
            // Console target
            var consoleTarget = new NLog.Targets.ConsoleTarget("console")
            {
                Layout = "${longdate}|${level:uppercase=true}|${message}"
            };
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTarget);

            // File target
            var fileTarget = new NLog.Targets.FileTarget("file")
            {
                FileName = "logs/api-tests-${shortdate}.log",
                Layout = "${longdate}|${level:uppercase=true}|${message}"
            };
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, fileTarget);

            LogManager.Configuration = config;
        }

        public static void Info(string message)
        {
            _logger.Info(message);
        }

        public static void Error(string message)
        {
            _logger.Error(message);
        }

        public static void Debug(string message)
        {
            _logger.Debug(message);
        }

        public static void Warning(string message)
        {
            _logger.Warn(message);
        }
    }
}