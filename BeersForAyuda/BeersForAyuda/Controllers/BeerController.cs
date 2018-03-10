using BeersForAyuda.Common;
using BeersForAyuda.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeersForAyuda.Controllers
{
    public class BeerController : Controller
    {
        public ActionResult Details(string Id)
        {
            try
            {
                List<Beer> beers = GetAllBeers();
                Beer selectedBeer = beers.Where(i => i.Id == Id).FirstOrDefault();
                return View(selectedBeer);
            }
            catch (System.Exception ex)
            {
                LogProvider.Error(ex.Message, 0);
                throw ex;
            }
        }
        
        private List<Beer> GetAllBeers()
        {
            return HttpRuntime.Cache["beers"] as List<Beer>;
        }
    }
}