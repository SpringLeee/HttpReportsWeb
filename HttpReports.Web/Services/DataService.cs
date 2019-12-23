using HttpReports.Web.DataAccessors;
using HttpReports.Web.DataContext;
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


    } 

}
