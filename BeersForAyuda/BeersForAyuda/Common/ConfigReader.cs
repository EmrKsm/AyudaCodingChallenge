using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeersForAyuda.Common
{
    public static class ConfigReader
    {
        public static string GetAppSettingWithName(string appSettingName)
        {
            try
            {
                return System.Configuration.ConfigurationManager.AppSettings.Get(appSettingName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}