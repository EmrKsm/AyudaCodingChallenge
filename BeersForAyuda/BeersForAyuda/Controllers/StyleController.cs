using BeersForAyuda.Common;
using BeersForAyuda.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeersForAyuda.Controllers
{
    public class StyleController : Controller
    {
        public static string _url = System.Configuration.ConfigurationManager.AppSettings["APIUrl"];

        public ActionResult Browse(int Id)
        {
            Style style = GetStyleById(Id);
            return View(style);
        }

        public List<Style> GetAllStyles()
        {
            return HttpRuntime.Cache["styles"] as List<Style>;
        }

        public Style GetStyleById(int Id)
        {
            Style style = null;
            var result = JsonConvert.DeserializeObject<StyleJson>(APICaller.CreateResponse($"{_url}style/{Id}/"));
            if (result.Status == "success")
            {
                style = result.Data;
            }
            var beers = HttpRuntime.Cache["beers"] as List<Beer>;
            style.Beers = new List<Beer>();
            foreach (var beer in beers.Where(b => b.StyleId == style.Id).ToList())
            {
                style.Beers.Add(beer);
            }
            return style;
        }
    }
}