using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HttpReports.Web.DataContext;
using HttpReports.Web.Implements;
using HttpReports.Web.Models;
using MySql.Data.MySqlClient;

namespace HttpReports.Web.DataAccessors
{
    public class DataAccessorMySql:IDataAccessor
    {
        private MySqlConnection conn;

        public DataAccessorMySql(DBFactory factory)
        {
            conn = factory.GetMySqlConnection();
        }

        public string BuildSqlWhere(GetIndexDataRequest request)
        {
            string where = " where 1=1 ";

            request.Start = request.Start.IsEmpty() ? DateTime.Now.ToString("yyyy-MM-dd") : request.Start;
            request.End = request.End.IsEmpty() ? DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") : request.End;

            if (!request.Node.IsEmpty())
            {
                string nodes = string.Join(",", request.Node.Split(",").ToList().Select(x => "'" + x + "'"));

                where = where + $" AND Node IN ({nodes})";
            }

            if (!request.Start.IsEmpty())
            {
                where = where + $" AND CreateTime >= '{request.Start}' ";
            }

            if (!request.End.IsEmpty())
            {
                where = where + $" AND CreateTime < '{request.End}' ";
            }

            return where;

        } 

        public List<EchartPineDataModel> GetStatusCode(GetIndexDataRequest request)
        {
            string where = BuildSqlWhere(request);

            string sql = $@"

                    Select '200' Name,COUNT(1) Value From  RequestInfo {where} AND StatusCode = 200
                    Union
                    Select '400' Name,COUNT(1) Value From  RequestInfo {where} AND StatusCode = 400
                    Union
                    Select '401' Name,COUNT(1) Value From  RequestInfo {where} AND StatusCode = 401
                    Union
                    Select '404' Name,COUNT(1) Value From  RequestInfo {where} AND StatusCode = 404
                    Union
                    Select '500' Name,COUNT(1) Value From  RequestInfo {where} AND StatusCode = 500

               ";

            return conn.Query<EchartPineDataModel>(sql).ToList();
        }


        public List<EchartPineDataModel> GetResponseTimePie(GetIndexDataRequest request)
        {
            string where = BuildSqlWhere(request);

            string sql = $@"  

                    Select Name,Value from ( 

                      Select 1 ID, '1-100' Name, Count(1) Value   From     RequestInfo {where} AND Milliseconds > 0 AND Milliseconds <= 100
                      union
                      Select 2 ID, '100-500' Name, Count(1) Value From   RequestInfo {where} AND Milliseconds > 100 AND Milliseconds <= 500
                      union
                      Select 3 ID, '500-1000' Name, Count(1) Value From  RequestInfo {where} AND Milliseconds > 500 AND Milliseconds <= 1000
                      union
                      Select 4 ID,'1000-3000' Name, Count(1) Value From  RequestInfo {where} AND Milliseconds > 1000 AND Milliseconds <= 3000
                      union
                      Select 5 Id,'3000-5000' Name, Count(1) Value From  RequestInfo {where} AND Milliseconds > 3000 AND Milliseconds <= 5000
                      union
                      Select 6 Id,'5000以上' Name, Count(1) Value From   RequestInfo {where} AND Milliseconds > 5000 

                  ) T Order By ID";

            return conn.Query<EchartPineDataModel>(sql).ToList();
        }

        public List<EchartPineDataModel> GetDayRequestTimes(GetIndexDataRequest request)
        {
            string where = " where 1=1 ";

            request.Day = request.Day.IsEmpty() ? DateTime.Now.ToString("yyyy-MM-dd") : request.Day;

            string End = request.Day.ToDateTime().AddDays(1).ToString("yyyy-MM-dd");

            if (!request.Node.IsEmpty())
            {
                string nodes = string.Join(",", request.Node.Split(",").ToList().Select(x => "'" + x + "'"));

                where = where + $" AND Node IN ({nodes})";
            }

            where = where + $" AND CreateTime >= '{request.Day}' AND CreateTime < '{End}'  ";


            string sql = $" Select Hour(CreateTime) Name ,COUNT(1) Value From RequestInfo {where} Group by  Hour(CreateTime)  ";

            return conn.Query<EchartPineDataModel>(sql).ToList();
        }


        public List<EchartPineDataModel> GetLatelyDayData(GetIndexDataRequest request)
        { 
            string where = " where 1=1 ";  

            if (!request.Node.IsEmpty())
            {
                string nodes = string.Join(",", request.Node.Split(",").ToList().Select(x => "'" + x + "'"));

                where = where + $" AND Node IN ({nodes})";
            }

            where = where + $" AND CreateTime >= '{request.Start}' AND CreateTime < '{request.End}'  ";


            string sql = $" Select DATE_FORMAT(CreateTime,'%Y-%m-%d') Name ,COUNT(1) Value From RequestInfo {where} Group by  DATE_FORMAT(CreateTime,'%Y-%m-%d')  ";

            return conn.Query<EchartPineDataModel>(sql).ToList();  

        }


        public List<EchartPineDataModel> GetMonthDataByYear(GetIndexDataRequest request)
        {
            string where = " where 1=1 ";

            if (!request.Node.IsEmpty())
            {
                string nodes = string.Join(",", request.Node.Split(",").ToList().Select(x => "'" + x + "'"));

                where = where + $" AND Node IN ({nodes})";
            }

            where = where + $" AND CreateTime >= '{request.Start}' AND CreateTime < '{request.End}'  ";

            string sql = $" Select DATE_FORMAT(CreateTime,'%Y-%m') Name,Count(1) Value From RequestInfo {where} Group by  DATE_FORMAT(CreateTime,'%Y-%m') ";

            return conn.Query<EchartPineDataModel>(sql).ToList();

        }



        public List<EchartPineDataModel> GetDayResponseTime(GetIndexDataRequest request)
        {
            string where = " where 1=1 ";

            request.Day = request.Day.IsEmpty() ? DateTime.Now.ToString("yyyy-MM-dd") : request.Day;

            string End = request.Day.ToDateTime().AddDays(1).ToString("yyyy-MM-dd");

            if (!request.Node.IsEmpty())
            {
                string nodes = string.Join(",", request.Node.Split(",").ToList().Select(x => "'" + x + "'"));

                where = where + $" AND Node IN ({nodes})";
            }

            where = where + $" AND CreateTime >= '{request.Day}' AND CreateTime < '{End}'  ";

            string sql = $" Select Hour(CreateTime) Name ,AVG(Milliseconds) Value From RequestInfo {where} Group by  Hour(CreateTime)   ";

            return conn.Query<EchartPineDataModel>(sql).ToList();
        }

        public List<string> GetNodes()
        {
            return conn.Query<string>(" Select Distinct Node  FROM RequestInfo ").ToList();
        }

        public GetIndexDataResponse GetIndexData(GetIndexDataRequest request)
        {
            string where = BuildSqlWhere(request);

            string sql = $@"

              Select  AVG(Milliseconds) ART From RequestInfo {where} ;
              Select  COUNT(1) Total From RequestInfo {where} ;
              Select  COUNT(1) Code404 From RequestInfo {where} AND StatusCode = 404 ;
              Select  COUNT(1) Code500 From RequestInfo {where} AND StatusCode = 500 ;
              Select Count(1) From ( Select Distinct Url From RequestInfo ) A;

           ";

            GetIndexDataResponse response = new GetIndexDataResponse();

            using (var result = conn.QueryMultiple(sql))
            {
                response.ART = (result.ReadFirstOrDefault<string>() ?? "0").ToDouble().ToString("0");
                response.Total = result.ReadFirstOrDefault<string>();
                response.Code404 = result.ReadFirstOrDefault<string>();
                response.Code500 = result.ReadFirstOrDefault<string>();
                response.APICount = result.ReadFirst<int>();
                response.ErrorPercent = response.Total.ToInt() == 0 ? "0.00%" : (Convert.ToDouble(response.Code500) / Convert.ToDouble(response.Total)).ToString("0.00%");
            }

            return response;
        }

        public List<GetTopResponse> GetTopRequest(GetTopRequest request)
        {
            string where = BuildTopWhere(request);

            string sql = $" Select  Url,COUNT(1) as Total From RequestInfo {where}  Group By Url order by Total {(request.IsDesc ? "Desc" : "Asc")} limit {request.TOP} ";

            return conn.Query<GetTopResponse>(sql).ToList();
        }

        public string BuildTopWhere(GetTopRequest request)
        {
            string where = " where 1=1 ";

            if (!request.Node.IsEmpty())
            {
                string nodes = string.Join(",", request.Node.Split(",").ToList().Select(x => "'" + x + "'"));

                where = where + $" AND Node IN ({nodes})";
            }

            if (!request.Start.IsEmpty())
            {
                where = where + $" AND CreateTime >= '{request.Start}' ";
            }

            if (!request.End.IsEmpty())
            {
                where = where + $" AND CreateTime < '{request.End}' ";
            }

            return where;
        }

        public List<GetTopResponse> GetCode500Response(GetTopRequest request)
        {
            string where = BuildTopWhere(request);

            string sql = $" Select   Url,COUNT(1) as Total From RequestInfo {where} AND StatusCode = 500 Group By Url order by Total {(request.IsDesc ? "Desc" : "Asc")} limit {request.TOP} ";

            return conn.Query<GetTopResponse>(sql).ToList();
        }


        /// <summary>
        /// 获取接口平均处理时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<EchartPineDataModel> GetTOPART(GetTopRequest request)
        {
            string where = BuildTopWhere(request);

            string sql = $" Select  Url Name ,Avg(Milliseconds) Value From RequestInfo {where} Group By Url order by Value {(request.IsDesc ? "Desc" : "Asc")} limit {request.TOP} ";

            return conn.Query<EchartPineDataModel>(sql).ToList();

        }

        public List<RequestInfo> GetRequestList(GetRequestListRequest request, out int totalCount)
        {
            string where = " where 1=1 ";

            if (!request.Node.IsEmpty())
            {
                string nodes = string.Join(",", request.Node.Split(",").ToList().Select(x => "'" + x + "'"));

                where = where + $" AND Node IN ({nodes})";
            }

            if (!request.Start.IsEmpty())
            {
                where = where + $" AND CreateTime >= '{request.Start}' ";
            }

            if (!request.End.IsEmpty())
            {
                where = where + $" AND CreateTime < '{request.End}' ";
            }

            if (!request.IP.IsEmpty())
            {
                where = where + $" AND IP = '{request.IP}' ";
            }

            if (!request.Url.IsEmpty())
            {
                where = where + $" AND  Url like '%{request.Url}%' ";
            }

            string sql = $"Select * From RequestInfo {where} limit {(request.pageNumber-1)* request.pageSize},{request.pageSize} ";

            totalCount = conn.QueryFirstOrDefault<int>(" Select count(1) From RequestInfo " + where);  

            return conn.Query<RequestInfo>(sql).ToList(); 
        } 
    }
}
