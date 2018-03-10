using LogHelper;
using System;
using System.Runtime.InteropServices;

namespace BeersForAyuda.Common
{
    public static class LogProvider
    {
        #region Properties
        private static string _logPattern = ConfigReader.GetAppSettingWithName(Resources.LogPattern);
        private static string _logPath = string.Format(ConfigReader.GetAppSettingWithName(Resources.LogPathConfigParameter), Resources.ApplicationName);
        private static string _datePattern = ConfigReader.GetAppSettingWithName(Resources.DatePattern);
        private static string _applicationName = Resources.ApplicationName;
        private static int _maxSizeRollBackups = Convert.ToInt32(Resources.MaxSizeRollBackups);
        private static string _maxFileSize = ConfigReader.GetAppSettingWithName(Resources.MaxFileSize);
        private static bool _staticLogFileName = Convert.ToBoolean(ConfigReader.GetAppSettingWithName(Resources.StaticLogFileName));
        private static bool _appendToFile = Convert.ToBoolean(ConfigReader.GetAppSettingWithName(Resources.AppendToFile));
        private static RollingFileLogHelper _rolFileLogger = new RollingFileLogHelper(file: _logPath,
                                                            pattern: _logPattern,
                                                            datePattern: _datePattern,
                                                            maxSizeRollBackups: _maxSizeRollBackups,
                                                            maxFileSize: _maxFileSize,
                                                            rollingMode: log4net.Appender.RollingFileAppender.RollingMode.Date,
                                                            staticLogFileName: _staticLogFileName,
                                                            appendToFile: _appendToFile);
        #endregion

        #region Methods
        public static void Error(string message, int eventId, [Optional]Exception ex)
        {
            try
            {
                _rolFileLogger.Error(message, eventId, ex);
            }
            catch (Exception e)
            {
                ManuelLogging(string.Format("Error occured when creating error log with log4net. Ex: {0}",
                                          e.Message));
                throw;
            }
        }
        public static void Warn(string message, int eventId, [Optional]Exception ex)
        {
            try
            {
                _rolFileLogger.Warn(message, eventId, ex);
            }
            catch (Exception e)
            {
                ManuelLogging(string.Format("Error occured when creating warning log with log4net. Ex: {0}",
                                          e.Message));
                throw;
            }
        }
        public static void Info(string message, [Optional] int eventId)
        {
            try
            {
                _rolFileLogger.Info(message);
            }
            catch (Exception e)
            {
                ManuelLogging(string.Format("Error occured when creating information log with log4net. Ex: {0}",
                                          e.Message));
                throw;
            }
        }
        private static void ManuelLogging(string message)
        {
            _rolFileLogger.ManuelLogging(message, _logPath);
        }
        #endregion
    }
}