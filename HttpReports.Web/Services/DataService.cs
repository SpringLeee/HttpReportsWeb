using HttpReports.Web.DataAccessors;
using HttpReports.Web.DataContext;
using HttpReports.Web.Implements;
using HttpReports.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpReports.Web.Services
{
    public class DataService
    { 
        private IDataAccessor _accessor;

        public DataService(IDataAccessor accessor)
        {
            _accessor = accessor;
        }

        public List<EchartPineDataModel> GetStatusCode(GetIndexDataRequest request) => _accessor.GetStatusCode(request);

        public List<EchartPineDataModel> GetResponseTimePie(GetIndexDataRequest request) => _accessor.GetResponseTimePie(request);

        public List<EchartPineDataModel> GetDayRequestTimes(GetIndexDataRequest request) => _accessor.GetDayRequestTimes(request); 

        public List<EchartPineDataModel> GetDayResponseTime(GetIndexDataRequest request) => _accessor.GetDayResponseTime(request);

        public List<string> GetNodes() => _accessor.GetNodes(); 

        public GetIndexDataResponse GetIndexData(GetIndexDataRequest request) => _accessor.GetIndexData(request);


        public List<GetTopResponse> GetTopRequest(GetTopRequest request) => _accessor.GetTopRequest(request);

        public List<GetTopResponse> GetCode500Response(GetTopRequest request) => _accessor.GetCode500Response(request);

        public List<EchartPineDataModel> GetTOPART(GetTopRequest request) => _accessor.GetTOPART(request);

        public List<RequestInfo> GetRequestList(GetRequestListRequest request, out int totalCount) => _accessor.GetRequestList(request,out totalCount);

        public List<EchartPineDataModel> GetLatelyDayData(GetIndexDataRequest request) => _accessor.GetLatelyDayData(request);

        public List<EchartPineDataModel> GetMonthDataByYear(GetIndexDataRequest request) => _accessor.GetMonthDataByYear(request); 

        public Result VaildJob(JobRequest request)
        {
            if (request.Title.IsEmpty() || request.Title.Length > 50)
            {
                return new Result(-1, "标题格式错误！");
            }

            if (request.Email.IsEmpty() || request.Email.Length > 300)
            {
                return new Result(-1, "邮箱格式错误！");
            }

            if (request.Node.IsEmpty())
            {
                return new Result(-1, "至少要选择一个服务节点！");
            }

            if (request.RtStatus > 0)
            {
                if (request.RtTime.IsEmpty() || request.RtRate.IsEmpty())
                {
                    return new Result(-1, "响应超时配置不能为空！");
                }

                if (!request.RtTime.IsNumber() || request.RtTime.ToInt() <= 0 || request.RtTime.ToInt() > 9999999)
                {
                    return new Result(-1, "响应超时配置 时间格式错误！");
                }

                if (!request.RtRate.Contains("%") || !request.RtRate.Replace("%","").IsNumber() || request.RtRate.Replace("%","").ToDouble() <= 0)
                {
                    return new Result(-1, "响应超时配置 超时率格式错误！");
                } 
            }

            if (request.HttpStatus > 0)
            {
                if (request.HttpCodes.IsEmpty() || request.HttpRate.IsEmpty())
                {
                    return new Result(-1, "请求错误配置 不能为空！");
                }

                int c = 0;

                request.HttpCodes.Replace("，", ",").Split(",").ToList().ForEach(x => {

                    if (!x.IsNumber())   c = 1;   
                });

                if (c == 1)
                {
                    return new Result(-1, "请求错误配置 HTTP状态码配置错误！");
                }

                if (!request.HttpRate.Contains("%") || !request.HttpRate.Replace("%", "").IsNumber() || request.HttpRate.Replace("%", "").ToDouble() <= 0)
                {
                    return new Result(-1, "请求错误配置 命中率格式错误！");
                }  
            }

            if (request.IPStatus > 0)
            {
                if (request.IPWhiteList.IsEmpty() || request.IPRate.IsEmpty())
                {
                    return new Result(-1, "IP配置不能为空！");
                } 

                if (!request.IPRate.Contains("%") || !request.IPRate.Replace("%","").IsNumber() || request.IPRate.Replace("%","").ToDouble() <= 0 )
                {
                    return new Result(-1, "IP配置 重复率格式错误！");
                }  
            }

            return new Result(1,"ok"); 
        }

        public string ParseJobRate(int rate)
        {
            if (rate == 1) return "0 0/1 * * * ?";
            if (rate == 3) return "0 0/3 * * * ?";
            if (rate == 5) return "0 0/5 * * * ?";
            if (rate == 10) return "0 0/10 * * * ?";
            if (rate == 30) return "0 0/30 * * * ?";
            if (rate == 60) return "0 0 0/1 * * ?";

            return "0 0/1 * * * ?";
        }

    } 

}
