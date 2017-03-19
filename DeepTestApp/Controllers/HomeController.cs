using DeepTest.Core;
using DeepTest.Infra;
using DeepTestApp.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System;
using PagedList;

namespace DeepTestApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly DeepService _deepServce;
        public JsonContainer MainResult { get; set; }

        public HomeController()
        {
            _deepServce = new DeepService();
        }
        public ActionResult Index()
        {
            return RedirectToAction("Search");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult AdvancedSearch()
        {
            return Json("");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Search(string name, string gender, int? page)
        {
            if (!page.HasValue)
            {
                SearchViewModel model = new SearchViewModel();
                model.PageNumber = 1;
                model.PageCount = 1;
                return View(model);
            }
            else
                return Search(new SearchViewModel() { Name = name, Gender = gender, PageNumber = page.Value }, page);
        }

        [HttpPost]
        public ActionResult Search(SearchViewModel model, int? page)
        {
            List<SearchResult> tempList = new List<SearchResult>();
            if (ModelState.IsValid)
            {
                MainResult = _deepServce.LoadFullJson();
                tempList = MainResult.people.Where(item => item.name.Contains(model.Name))
                    .Select(item => new SearchResult
                    {
                        BirthPlace = getPlaceName(Convert.ToInt32(item.place_id)),
                        Gender = string.IsNullOrEmpty(item.gender) ? "" : (item.gender == "M" ? "Male" : "Female"),
                        Id = item.id,
                        Name = item.name
                    }).ToList();

                if (!string.IsNullOrEmpty(model.Gender))
                    tempList = tempList.Where(item => item.Gender.ToLower() == (model.Gender.ToLower() == "m" ? "male" : "female")).ToList();
                model.PageNumber = page ?? 1;
                model.PageCount = tempList.Count;
                model.Results = tempList.ToPagedList(page ?? 1, 10);
            }
            return View(model);
        }

        private string getPlaceName(int id)
        {
            return MainResult.places.Where(item => item.id == id).Single().name;
        }
    }
}