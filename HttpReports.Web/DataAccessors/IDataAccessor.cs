using HttpReports.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpReports.Web.DataAccessors
{
    public interface IDataAccessor
    {
        string BuildSqlWhere(GetIndexDataRequest request);

        List<EchartPineDataModel> GetStatusCode(GetIndexDataRequest request);

        List<EchartPineDataModel> GetResponseTimePie(GetIndexDataRequest request);

        List<EchartPineDataModel> GetDayRequestTimes(GetIndexDataRequest request);

        List<EchartPineDataModel> GetDayResponseTime(GetIndexDataRequest request);

        List<string> GetNodes();

        GetIndexDataResponse GetIndexData(GetIndexDataRequest request);

        List<EchartPineDataModel> GetLatelyDayData(GetIndexDataRequest request);

        List<GetTopResponse> GetTopRequest(GetTopRequest request);

        string BuildTopWhere(GetTopRequest request);

        List<GetTopResponse> GetCode500Response(GetTopRequest request);

        List<EchartPineDataModel> GetTOPART(GetTopRequest request); 

        List<RequestInfo> GetRequestList(GetRequestListRequest request, out int totalCount);

        List<EchartPineDataModel> GetMonthDataByYear(GetIndexDataRequest request);
    }
}
