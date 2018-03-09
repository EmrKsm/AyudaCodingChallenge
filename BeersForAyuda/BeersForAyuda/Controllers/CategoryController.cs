using BeersForAyuda.Common;
using BeersForAyuda.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BeersForAyuda.Controllers
{
    public class CategoryController : Controller
    {
        public static string _url = System.Configuration.ConfigurationManager.AppSettings["APIUrl"];
        // GET: Category
        public ActionResult Index()
        {
            APICaller.LoadCacheData();
            List<Category> categories = GetAllCategories();
            return View(categories);
        }

        public ActionResult Browse(int Id)
        {
            Category category = GetCategoryById(Id);
            return View(category);
        }

        public List<Category> GetAllCategories()
        {
            List<Category> categories = null;
            List<Style> styles = GetAllStyles();

            var result = JsonConvert.DeserializeObject<CategoriesJson>(APICaller.CreateResponse($"{_url}categories/"));
            if (result.Status == "success")
            {
                categories = result.Data;
            }
            foreach (var category in categories)
            {
                category.Styles = styles.Where(s=>s.CategoryId == category.Id).ToList();
            }
            return categories;
        }
        public Category GetCategoryById(int Id)
        {
            Category category = null;
            var result = JsonConvert.DeserializeObject<CategoryJson>(APICaller.CreateResponse($"{_url}category/{Id}/"));
            if (result.Status == "success")
            {
                category = result.Data;
            }
            var styles = HttpRuntime.Cache["styles"] as List<Style>;
            category.Styles = new List<Style>();
            foreach (var style in styles.Where(c => c.CategoryId == category.Id).ToList())
            {
                category.Styles.Add(style);
            }
            return category;
        }
        public List<Style> GetAllStyles()
        {
            return HttpRuntime.Cache["styles"] as List<Style>;
        }
    }
}