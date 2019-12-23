using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpReports.Web.Implements;
using HttpReports.Web.Models;
using HttpReports.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace HttpReports.Web.Controllers
{
    public class DataController : Controller
    {   
        private DataService _dataService;

        private List<int> hours = new List<int>  { 0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23  };   

        public DataController(DataService dataService)
        {
            _dataService = dataService;
        }

        public IActionResult GetIndexChartA(GetIndexDataRequest request)
        {  
            request.Start = request.Start.IsEmpty() ? DateTime.Now.ToString("yyyy-MM-dd") : request.Start;
            request.End = request.End.IsEmpty() ? DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") : request.End; 

            var TopRequest = _dataService.GetTopRequest(new Models.GetTopRequest { 
                Node = request.Node,
                Start =request.Start,
                End = request.End,
                IsDesc = true,
                TOP = request.TOP
            }); 

            var TopError500= _dataService.GetCode500Response(new Models.GetTopRequest
            {
                Node = request.Node,
                Start = request.Start,
                End = request.End,
                IsDesc = true,
                TOP = request.TOP
            }); 

            var fast = _dataService.GetTOPART(new Models.GetTopRequest
            {
                Node = request.Node,
                Start = request.Start,
                End = request.End,
                IsDesc = false,
                TOP = request.TOP
            });

            var slow = _dataService.GetTOPART(new Models.GetTopRequest
            {
                Node = request.Node,
                Start = request.Start,
                End = request.End,
                IsDesc = true,
                TOP = request.TOP
            });

            var Art = new {  fast,slow}; 

            var StatusCode = _dataService.GetStatusCode(request);

            var ResponseTime = _dataService.GetResponseTimePie(request);

            return Json(new Result(1, "ok",new {   StatusCode , ResponseTime, TopRequest, TopError500, Art })); 
        } 


        public IActionResult GetStatusCodePie(GetIndexDataRequest request)
        {
            var data = _dataService.GetStatusCode(request);

            return Json(new Result(1,"ok",data)); 
        }

        public IActionResult GetResponseTimePie(GetIndexDataRequest request)
        {
            var data = _dataService.GetResponseTimePie(request);

            return Json(new Result(1, "ok", data)); 
        }

        public IActionResult GetDayStateBar(GetIndexDataRequest request)
        {
            List<int> timesList = new List<int>();

            List<int> avgList = new List<int>();


            // 每小时请求次数
            List<EchartPineDataModel> times = _dataService.GetDayRequestTimes(request); 

            //每小时平均处理时间
            List<EchartPineDataModel> avg = _dataService.GetDayResponseTime(request); 

            foreach (var item in hours)
            {
                // 每小时请求次数
                var timeModel = times.Where(x => x.Name == item.ToString()).FirstOrDefault(); 
                timesList.Add(timeModel == null ? 0:timeModel.Value);

                //每小时平均处理时间
                var avgModel = avg.Where(x => x.Name == item.ToString()).FirstOrDefault(); 
                avgList.Add(avgModel == null ? 0:avgModel.Value);  
            }

            return Json(new Result(1, "ok", new { timesList , avgList ,hours }));
        }

        public IActionResult GetLatelyDayChart(GetIndexDataRequest request)
        { 
            if (request.Month.IsEmpty())
            {
                request.Month = DateTime.Now.ToString("yyyy-MM");
            }

            request.Start = request.Month + "-01";
            request.End = (request.Month + "-01").ToDateTime().AddMonths(1).ToString("yyyy-MM-dd");
            
             
            var list = _dataService.GetLatelyDayData(request);

            List<string> time = new List<string>();
            List<int> value = new List<int>();

            string Range = request.Start.ToDateTime().ToString("yyyy-MM-dd") + " - " + request.End.ToDateTime().AddDays(-1).ToString("yyyy-MM-dd"); 

            for (int i = 0; i < (request.End.ToDateTime() - request.Start.ToDateTime()).Days ; i++)
            {
                DateTime k = request.Start.ToDateTime().AddDays(i);

                var j = list.Where(x => x.Name == k.ToString("yyyy-MM-dd")).FirstOrDefault();

                if (j != null)
                {
                    time.Add(k.ToString("dd"));
                    value.Add(j.Value);
                }
                else
                {
                    time.Add(k.ToString("dd"));
                    value.Add(0); 
                }  
            }  

            return Json(new Result(1,"ok",new { time,value,Range })); 
        }


        public IActionResult GetMonthDataByYear(GetIndexDataRequest request)
        {
            if (request.Year.IsEmpty())
            {
                request.Year = DateTime.Now.ToString("yyyy");
            }

            request.Start = request.Year + "-01-01";
            request.End = ( (request.Year.ToInt() + 1) + "-01-01").ToDateTime().ToString("yyyy-MM-dd");

            string Range = request.Start.ToDateTime().ToString("yyyy-MM") + " - " + request.End.ToDateTime().AddDays(-1).ToString("yyyy-MM");

            var list = _dataService.GetMonthDataByYear(request);

            List<string> time = new List<string>();
            List<int> value = new List<int>();

            for (int i = 0; i < 12; i++)
            {
                DateTime k = request.Start.ToDateTime().AddMonths(i);

                var j = list.Where(x => x.Name == k.ToString("yyyy-MM")).FirstOrDefault();

                if (j != null)
                {
                    time.Add(k.ToString("yyyy-MM"));
                    value.Add(j.Value);
                }
                else
                {
                    time.Add(k.ToString("yyyy-MM"));
                    value.Add(0);
                }
            }

            return Json(new Result(1, "ok", new { time, value, Range })); 

        } 


        public IActionResult GetTimeRange(int Tag)
        { 
            if (Tag == 1)
            {
                return Json(new Result(1,"ok" , new { 
                 
                    start = DateTime.Now.ToString("yyyy-MM-dd"),
                    end = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")

                })); 
            }

            if (Tag == 2)
            {
                return Json(new Result(1, "ok", new
                {

                    start = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"),
                    end = DateTime.Now.ToString("yyyy-MM-dd")

                }));
            }

            if (Tag == 3)
            {
                return Json(new Result(1, "ok", new
                {

                    start = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + 1).ToString("yyyy-MM-dd"),
                    end = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")

                }));
            }

            if (Tag == 4)
            {
                return Json(new Result(1, "ok", new
                {

                    start = DateTime.Now.AddDays(-DateTime.Now.Day+1).ToString("yyyy-MM-dd"),
                    end = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")

                }));
            } 

            return NoContent(); 
        }

        public IActionResult GetTimeTag(string start,string end,int TagValue)
        { 
            Result result = new Result(1, "ok", 0);

            if (TagValue > 0)
            {
                if (TagValue == 1 && start == DateTime.Now.ToString("yyyy-MM-dd") && end == DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"))
                {
                    return Json(new Result(1, "ok", -1));
                }

                if (TagValue == 2 && start == DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") && end == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    return Json(new Result(1, "ok", -1));
                }

                if (TagValue == 3 && start == DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + 1).ToString("yyyy-MM-dd") && end == DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"))
                {
                    return Json(new Result(1, "ok", -1));
                }

                if (TagValue == 4 && start == DateTime.Now.AddDays(-DateTime.Now.Day + 1).ToString("yyyy-MM-dd") && end == DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"))
                {
                    return Json(new Result(1, "ok", -1));
                } 
            }

            if (start.IsEmpty() && end.IsEmpty())
            {
                result = new Result(1, "ok", 1);

                return Json(result);
            }


            if (start == DateTime.Now.ToString("yyyy-MM-dd") && end == DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"))
            {
                result = new Result(1, "ok", 1);

                return Json(result);
            }

            if (start == DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") && end == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                result = new Result(1, "ok", 2);
                return Json(result);
            }

            if (start == DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + 1).ToString("yyyy-MM-dd") && end == DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"))
            {
                result = new Result(1, "ok", 3);
                return Json(result);

            }

            if (start == DateTime.Now.AddDays(-DateTime.Now.Day + 1).ToString("yyyy-MM-dd") && end == DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"))
            {
                result = new Result(1, "ok", 4);
                return Json(result);
            }   


            return Json(result); 
        }  
        public IActionResult GetNodes()
        {
            var nodes = _dataService.GetNodes();

            return Json(new Result(1, "ok",nodes)); 
        } 

        public IActionResult GetIndexData(GetIndexDataRequest request)
        { 
            var result = _dataService.GetIndexData(request);

            return Json(new Result(1,"ok",result));
        }

        public IActionResult GetTopRequest(GetTopRequest request)
        {
            request.Start = request.Start.IsEmpty() ? DateTime.Now.ToString("yyyy-MM-dd") : request.Start;
            request.End = request.End.IsEmpty() ? DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") : request.End;


            var most = _dataService.GetTopRequest(new Models.GetTopRequest { 
                Node = request.Node,
                Start =request.Start,
                End = request.End,
                IsDesc = true,
                TOP = request.TOP
            });

            var least = _dataService.GetTopRequest(new Models.GetTopRequest
            {
                Node = request.Node,
                Start = request.Start,
                End = request.End,
                IsDesc = false,
                TOP = request.TOP
            });

            return Json(new Result(1, "ok", new { most,least })); 
        }

        public IActionResult GetTopCode500(GetTopRequest request)
        {
            request.Start = request.Start.IsEmpty() ? DateTime.Now.ToString("yyyy-MM-dd") : request.Start;
            request.End = request.End.IsEmpty() ? DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") : request.End;

            var data = _dataService.GetCode500Response(new Models.GetTopRequest
            {
                Node = request.Node,
                Start = request.Start,
                End = request.End,
                IsDesc = true,
                TOP = request.TOP
            });  

            return Json(new Result(1, "ok", data)); 

        }

        public IActionResult GetTOPART(GetTopRequest request)
        { 
            request.Start = request.Start.IsEmpty() ? DateTime.Now.ToString("yyyy-MM-dd") : request.Start;
            request.End = request.End.IsEmpty() ? DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") : request.End; 

            var fast = _dataService.GetTOPART(new Models.GetTopRequest
            {
                Node = request.Node,
                Start = request.Start,
                End = request.End,
                IsDesc = false,
                TOP = request.TOP
            }); 

            var slow = _dataService.GetTOPART(new Models.GetTopRequest
            {
                Node = request.Node,
                Start = request.Start,
                End = request.End,
                IsDesc = true,
                TOP = request.TOP
            });


            return Json(new Result(1, "ok",new { fast,slow }));  
        }

        public IActionResult GetRequestList(GetRequestListRequest request)
        {
            int totalCount = 0;

            if (request.Start.IsEmpty() && request.End.IsEmpty())
            {
                request.Start = DateTime.Now.ToString("yyyy-MM-dd");

                request.End = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"); 

            } 

            var list = _dataService.GetRequestList(request,out totalCount); 

            return Json(new { total = totalCount, rows = list });

        }   
    }
}