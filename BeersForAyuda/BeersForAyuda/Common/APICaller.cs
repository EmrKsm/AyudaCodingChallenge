using BeersForAyuda.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Web.Caching;

namespace BeersForAyuda.Common
{
    public static class APICaller
    {
        public static string _url = System.Configuration.ConfigurationManager.AppSettings["APIUrl"];

        public static void LoadCacheData()
        {
            List<Style> styles = null;
            List<Beer> beers = null;

            try
            {
                if (HttpRuntime.Cache.Get("styles") == null)
                {
                    var stylesResult = JsonConvert.DeserializeObject<StylesJson>(CreateResponse($"{_url}styles/"));
                    if (stylesResult.Status == "success")
                    {
                        styles = stylesResult.Data;
                    }
                    HttpRuntime.Cache.Insert(
                            "styles",
                            styles,
                            null,
                            DateTime.Now.AddDays(1),
                            Cache.NoSlidingExpiration,
                            CacheItemPriority.NotRemovable,
                            null);
                }
                if (HttpRuntime.Cache.Get("beers") == null)
                {
                    var beersResult = JsonConvert.DeserializeObject<BeersJson>(CreateResponse($"{_url}beers/"));
                    if (beersResult.Status == "success")
                    {
                        beers = beersResult.Data;
                    }
                    HttpRuntime.Cache.Insert(
                            "beers",
                            beers,
                            null,
                            DateTime.Now.AddDays(1),
                            Cache.NoSlidingExpiration,
                            CacheItemPriority.NotRemovable,
                            null);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string CreateResponse(string uri)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var builder = new UriBuilder(uri);
                    builder.Port = -1;
                    var query = HttpUtility.ParseQueryString(builder.Query);
                    query["key"] = System.Configuration.ConfigurationManager.AppSettings["APIKey"];
                    builder.Query = query.ToString();
                    string url = builder.ToString();
                    return httpClient.GetStringAsync(new Uri(url)).Result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}