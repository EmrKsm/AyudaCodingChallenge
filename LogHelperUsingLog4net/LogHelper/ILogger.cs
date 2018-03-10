using System;
using System.Runtime.InteropServices;

namespace LogHelper
{
    interface ILogger
    {
        void Error(string message, [Optional] int? eventId, [Optional] Exception ex);
        void Warn(string message, [Optional] int? eventId, [Optional] Exception ex);
        void Info(string message, [Optional] int? eventId);
        void ManuelLogging(string message, string logDirectory);
    }
}
