using NLog;

namespace SimpleWebApi.Helpers
{
    public class LoggerManager : ILoggerManager
    {
        private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();
        public void LogDebug(string message) => logger.Debug(string.Concat("Logger: ", logger.Name, " Message: ", message));
        public void LogError(string message) => logger.Error(string.Concat("Logger: ", logger.Name, " Message: ", message));
        public void LogInfo(string message) => logger.Info(string.Concat("Logger: ", logger.Name, " Message: ", message));
        public void LogWarn(string message) => logger.Warn(string.Concat("Logger: ", logger.Name, " Message: ", message));
    }
}
