using BeersForAyuda.Common;
using BeersForAyuda.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeersForAyuda.Controllers
{
    public class HomeController : Controller
    {
        public static string _url = System.Configuration.ConfigurationManager.AppSettings["APIUrl"];
        public ActionResult Index()
        {
            APICaller.LoadCacheData();
            List<Beer> beers = GetAllBeers();
            return View(GetRandomBeers(beers));
        }

        public ActionResult Browse(string sortOrder, string CurrentSort, string searchString)
        {
            try
            {
                APICaller.LoadCacheData();
                List<Beer> beers = GetAllBeers();

                if (!String.IsNullOrEmpty(searchString))
                {
                    beers = beers.Where(s => s.Name.Contains(searchString)) as List<Beer>;
                }
                ViewBag.CurrentSort = sortOrder;
                sortOrder = String.IsNullOrEmpty(sortOrder) ? "Name" : sortOrder;
                switch (sortOrder)
                {
                    case "Name":
                        if (sortOrder.Equals(CurrentSort))
                            beers = beers.OrderByDescending(b => b.Name).ToList();
                        else
                            beers = beers.OrderBy(b => b.Name).ToList();
                        break;
                    case "IsOrganic ":
                        if (sortOrder.Equals(CurrentSort))
                            beers = beers.OrderByDescending(b => b.IsOrganic).ToList();
                        else
                            beers = beers.OrderBy(b => b.IsOrganic).ToList();
                        break;
                    case "Default":
                        beers = beers.OrderBy(b => b.Name).ToList();
                        break;
                }
                return View(beers);
            }
            catch (Exception ex)
            {
                LogProvider.Error(ex.Message, 0);
                throw ex;
            }
        }

        private List<Beer> GetAllBeers()
        {
            try
            {
                return HttpRuntime.Cache["beers"] as List<Beer>;
            }
            catch (Exception ex)
            {
                LogProvider.Error(ex.Message, 0);
                throw ex;
            }
        }

        private List<Beer> GetRandomBeers(List<Beer> beers)
        {
            try
            {
                List<Beer> randomBeers = new List<Beer>();
                List<Beer> listToView = new List<Beer>();
                randomBeers = beers.Randomize().ToList();
                //Clear list from not available beers and for beer not have labels
                randomBeers.RemoveAll(b => b.AvailableId == null);
                foreach (var beer in randomBeers)
                {
                    listToView.Add(beer);
                    if (listToView.Count == 6)
                    {
                        break;
                    }
                }
                return listToView;
            }
            catch (Exception ex)
            {
                LogProvider.Error(ex.Message, 0);
                throw ex;
            }
        }
    }
}