using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HttpReports.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace HttpReports.Web.Controllers
{
    public class TestController : Controller
    {
        private DataService _dataService;

        public TestController(DataService dataService)
        {
            _dataService = dataService;
        }

        public IActionResult Index()
        {
            for (int i = 0; i < 1000; i++)
            {
                SqlConnection con = new SqlConnection("server=.;uid=sa;pwd=123456;database=HttpReports;Connect Timeout=600");

                con.Query(" select count(1) from [HttpReports].[dbo].[RequestInfo] ");  

            }  

            return Content(DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));
        }
    }
}