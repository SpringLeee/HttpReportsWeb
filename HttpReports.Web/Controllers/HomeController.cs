using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpReports.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace HttpReports.Web.Controllers
{
    public class HomeController : Controller
    { 
        private DataService _dataService;

        public HomeController(DataService dataService)
        {
            _dataService = dataService;
        } 

        public IActionResult Index()
        {
            var nodes = _dataService.GetNodes();

            ViewBag.nodes = nodes;

            return View();
        }

        public IActionResult Trend()
        {
            var nodes = _dataService.GetNodes();

            ViewBag.nodes = nodes;

            return View();
        }



        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }

    }
}