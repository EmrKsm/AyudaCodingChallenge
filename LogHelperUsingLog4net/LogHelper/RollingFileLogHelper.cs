using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace LogHelper
{
    public class RollingFileLogHelper : ILogger
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(RollingFileLogHelper));
        /// <summary>
        /// Create rolling file object with given parameters
        /// </summary>
        /// <param name="file">Log file path includes file name without extension and date</param>
        /// <param name="pattern">Logging pattern defined in config</param>
        /// <param name="datePattern">Dating pattern</param>
        /// <param name="maxSizeRollBackups">Max number of rolled files exeeds size</param>
        /// <param name="maxFileSize">File size</param>
        /// <param name="rollingMode">Style of rolling, date vs.</param>
        /// <param name="staticLogFileName">Static name</param>
        /// <param name="appendToFile">Appending file</param>
        public RollingFileLogHelper(string file, string pattern, string datePattern , int maxSizeRollBackups, string maxFileSize, RollingFileAppender.RollingMode rollingMode, bool staticLogFileName, bool appendToFile)
        {
            try
            {
                Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
                PatternLayout patternLayout = new PatternLayout();
                patternLayout.ConversionPattern = pattern;
                patternLayout.ActivateOptions();
                RollingFileAppender rollingFileAppender = new RollingFileAppender();
                rollingFileAppender.AppendToFile = appendToFile;
                rollingFileAppender.RollingStyle = rollingMode;
                rollingFileAppender.DatePattern = datePattern;
                rollingFileAppender.File = file;
                rollingFileAppender.Layout = patternLayout;
                rollingFileAppender.MaximumFileSize = maxFileSize;
                rollingFileAppender.MaxSizeRollBackups = maxSizeRollBackups;
                rollingFileAppender.StaticLogFileName = staticLogFileName;
                rollingFileAppender.LockingModel = new FileAppender.MinimalLock();
                hierarchy.Root.AddAppender(rollingFileAppender);
                rollingFileAppender.ActivateOptions();
                hierarchy.Root.Level = Level.All;
                hierarchy.Configured = true;
            }
            catch (Exception ex)
            {
                string logMessage = string.Format("{0} - Error occured when creating rolling file appender for log4net. Ex: {1}", MethodBase.GetCurrentMethod().Name, ex.Message);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        /// <summary>
        /// Create Error level log to file
        /// </summary>
        /// <param name="message">Message context of event log</param>
        /// <param name="eventId">Event ID of event log is optional</param>
        /// <param name="ex">Log exception is optional parameter</param>
        public void Error(string message, int? eventId = null, Exception ex = null)
        {
            try
            {
                if (ex != null)
                {
                    logger.Error(message, ex);
                }
                else
                {
                    logger.Error(message);
                }
            }
            catch (Exception e)
            {
                string logMessage = string.Format("{0} - Error occured when creating error log for log4net. Ex: {1}", MethodBase.GetCurrentMethod().Name, e.Message);
                throw new Exception(e.Message, e.InnerException);
            }
        }
        /// <summary>
        /// Create Warn level log file 
        /// </summary>
        /// <param name="message">Message context of event log</param>
        /// <param name="eventId">Event ID of event log</param>
        /// <param name="ex">Log exception is optional parameter</param>
        public void Warn(string message, int? eventId = null, Exception ex = null)
        {
            try
            {
                if (ex != null)
                {
                    logger.Warn(message, ex);
                }
                else
                {
                    logger.Warn(message);
                }
            }
            catch (Exception e)
            {
                string logMessage = string.Format("{0} - Error occured when creating warning log for log4net. Ex: {1}", MethodBase.GetCurrentMethod().Name, e.Message);
                throw new Exception(logMessage, e.InnerException);
            }
        }
        /// <summary>
        /// Create Info level log file 
        /// </summary>
        /// <param name="message">Message context of event log</param>
        /// <param name="eventId">Event ID of event log is optional</param>
        public void Info(string message, int? eventId = null)
        {
            try
            {
                logger.Info(message);
            }
            catch (Exception ex)
            {
                string logMessage = string.Format("{0} - Error occured when creating information log for log4net. Ex: {1}", MethodBase.GetCurrentMethod().Name, ex.Message);
                throw new Exception(logMessage, ex.InnerException);
            }
        }
        /// <summary>
        /// Create manuel log to file if smth happens in log4net
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="logDirectory">Log file path includes file name without extension and date</param>
        public void ManuelLogging(string message, string logDirectory)
        {
            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);
            string logFile = String.Format("{0}-{1}.log", logDirectory, DateTime.Now.ToString("dd.MM.yy"));
            if (File.Exists(logFile))
                using (FileStream fs = new FileStream(logFile, FileMode.Append))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine("-> " + DateTime.Now + " => " + message);
                        sw.Close();
                    }
                    fs.Close();
                }
            else
                using (FileStream fs = new FileStream(logFile, FileMode.OpenOrCreate))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine("-> " + DateTime.Now + " => " + message);
                        sw.Close();
                    }
                    fs.Close();
                }
        }
    }
}

