using System;
using System.Web;
using System.Net.Http;


namespace BeersForAyuda.Common
{
    public static class APICaller
    {
        public static string _url = System.Configuration.ConfigurationManager.AppSettings["APIUrl"];

        public static void LoadCacheData()
        {
           
        }

        public static string CreateResponse(string uri)
        {
            using (var httpClient = new HttpClient())
            {
                var builder = new UriBuilder(uri);
                builder.Port = -1;
                var query = HttpUtility.ParseQueryString(builder.Query);
                query["key"] = System.Configuration.ConfigurationManager.AppSettings["APIKey"];
                builder.Query = query.ToString();
                string url = builder.ToString();
                return httpClient.GetStringAsync(new Uri(url)).Result;
            }
        }
    }
}