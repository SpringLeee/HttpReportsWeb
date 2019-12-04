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

        public IActionResult Index()
        {  
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }  
    }
}