using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace LogHelper
{
    public class EventLogHelper : ILogger
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(EventLogHelper));
        /// <summary>
        /// Create event logger class with given parameters
        /// </summary>
        /// <param name="logName">Event log application name</param>
        /// <param name="appName">Event log source name</param>
        /// <param name="pattern">Log text pattern for log4net</param>
        public EventLogHelper(string logName,
                                string appName,
                                string pattern)
        {
            try
            {
                Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
                PatternLayout patternLayout = new PatternLayout();
                patternLayout.ConversionPattern = pattern;
                patternLayout.ActivateOptions();

                EventLogAppender eventLogger = new EventLogAppender();
                eventLogger.LogName = logName;
                eventLogger.ApplicationName = appName;
                eventLogger.Layout = patternLayout;
                eventLogger.ActivateOptions();
                hierarchy.Root.AddAppender(eventLogger);

                hierarchy.Root.Level = Level.All;
                hierarchy.Configured = true;
            }
            catch (Exception ex)
            {
                string logMessage = String.Format("{0} - Error occured when creating event log appender for log4net. Ex: {1}",
                                                   System.Reflection.MethodBase.GetCurrentMethod().Name,
                                                   ex.Message);
                throw new Exception(logMessage, ex.InnerException);
            }
        }
        /// <summary>
        /// Create Error level event log with given event id or default 0
        /// </summary>
        /// <param name="message">Message context of event log</param>
        /// <param name="eventId">Event ID of event log</param>
        /// <param name="ex">Log exception is optional parameter</param>
        public void Error(string message, [Optional] int? eventId, [Optional] Exception ex)
        {
            try
            {
                if (eventId != null)
                    log4net.ThreadContext.Properties["EventID"] = eventId;
                if (ex != null)
                    logger.Error(message, ex);
                else
                    logger.Error(message);
            }
            catch (Exception e)
            {
                string logMessage = String.Format("{0} - Error occured when creating error log for log4net. Ex: {1}",
                                                   System.Reflection.MethodBase.GetCurrentMethod().Name,
                                                   e.Message);
                throw new Exception(logMessage, e.InnerException);
            }
        }
        /// <summary>
        /// Create Warn level event log with given event id or default 0
        /// </summary>
        /// <param name="message">Message context of event log</param>
        /// <param name="eventId">Event ID of event log</param>
        /// <param name="ex">Log exception is optional parameter</param>
        public void Warn(string message, [Optional] int? eventId, [Optional] Exception ex)
        {
            try
            {
                if (eventId != null)
                    log4net.ThreadContext.Properties["EventID"] = eventId;
                if (ex != null)
                    logger.Warn(message, ex);
                else
                    logger.Warn(message);
            }
            catch (Exception e)
            {
                string logMessage = String.Format("{0} - Error occured when creating warning log for log4net. Ex: {1}",
                                                   System.Reflection.MethodBase.GetCurrentMethod().Name,
                                                   e.Message);
                throw new Exception(logMessage, e.InnerException);
            }
        }
        /// <summary>
        /// Create Info level event log with given event id or default 0
        /// </summary>
        /// <param name="message">Message context of event log</param>
        /// <param name="eventId">Event ID of event log is optional</param>
        public void Info(string message, [Optional] int? eventId)
        {
            try
            {
                if (eventId != null)
                    log4net.ThreadContext.Properties["EventID"] = eventId;
                logger.Info(message);
            }
            catch (Exception e)
            {
                string logMessage = String.Format("{0} - Error occured when creating information log for log4net. Ex: {1}",
                                                   System.Reflection.MethodBase.GetCurrentMethod().Name,
                                                   e.Message);
                throw new Exception(logMessage, e.InnerException);
            }
        }
        /// <summary>
        /// Create log file manuelly when log4net is not working
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="logDirectory">Log directory without file name</param>
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
