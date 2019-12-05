using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HttpReports.Web.DataContext
{
    public class DBFactory
    { 

        public IConfiguration _configuration; 

        public DBFactory(IConfiguration configuration)
        {
            _configuration = configuration; 
        } 
        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("HttpReports"));
        }

        public MySqlConnection GetMySqlConnection()
        {
            return new MySqlConnection(_configuration.GetConnectionString("HttpReports"));
        }

        public void InitDB()
        { 
            string DBType = _configuration["HttpReportsConfig:DBType"];

            string Constr = _configuration.GetConnectionString("HttpReports");

            if (string.IsNullOrEmpty(DBType) || string.IsNullOrEmpty(Constr) )
            {  
                throw new Exception("数据库类型配置错误!"); 
            }

            try
            { 
                if (DBType.ToLower() == "sqlserver") InitSqlServer(Constr);
                if (DBType.ToLower() == "mysql") InitMySql(Constr);

            }
            catch (Exception ex)
            { 
                throw new Exception("数据库初始化失败！错误信息:" + ex.Message);
            } 

        }

        private void InitSqlServer(string Constr)
        { 
            using (SqlConnection con = new SqlConnection(Constr))
            {
                //string TempConstr = Constr.Replace("httpreports", "master");   

                //string DB_id = con.QueryFirstOrDefault<string>(" SELECT DB_ID('HttpReports') ");

                //if (string.IsNullOrEmpty(DB_id))
                //{
                //    int i = con.Execute(" Create Database HttpReports ");
                //}

                int TableCount = con.QueryFirstOrDefault<int>(" Use HttpReports; Select Count(*) from sysobjects where id = object_id('HttpReports.dbo.RequestInfo') ");

                if (TableCount == 0)
                {
                    con.Execute(@"  

                        USE [HttpReports];
                        SET ANSI_NULLS ON;
                        SET QUOTED_IDENTIFIER ON;
                        CREATE TABLE [dbo].[RequestInfo](
	                        [Id] [int] IDENTITY(1,1) NOT NULL,
	                        [Node] [nvarchar](50) NOT NULL,
	                        [Route] [nvarchar](50) NOT NULL,
	                        [Url] [nvarchar](200) NOT NULL,
	                        [Method] [nvarchar](50) NOT NULL,
	                        [Milliseconds] [int] NOT NULL,
	                        [StatusCode] [int] NOT NULL,
	                        [IP] [nvarchar](50) NOT NULL,
	                        [CreateTime] [datetime] NOT NULL
                        ) ON [PRIMARY];

                    ");

                    MockSqlServer(Constr);
                }

            } 
        }

        private void InitMySql(string Constr)
        {  
            using (MySqlConnection con = new MySqlConnection(Constr))
            {
                //string TempConstr = Constr.Replace("httpreports","sys");

                //MySqlConnection TempConn = new MySqlConnection(TempConstr); 
                
                //var DbInfo = TempConn.QueryFirstOrDefault<string>("  show databases like 'httpreports'; ");    

                //if (string.IsNullOrEmpty(DbInfo))
                //{
                //    TempConn.Execute(" create database HttpReports; ");
                //}

                //TempConn.Close();
                //TempConn.Dispose(); 

                var TableInfo = con.QueryFirstOrDefault<int>("  Select count(1) from information_schema.tables where table_name ='requestinfo'; ");

                if (TableInfo == 0)
                {
                    con.Execute(@"
                        CREATE TABLE `requestinfo` (
                          `Id` int(11) NOT NULL auto_increment,
                          `Node` varchar(50) default NULL,
                          `Route` varchar(50) default NULL,
                          `Url` varchar(200) default NULL,
                          `Method` varchar(50) default NULL,
                          `Milliseconds` int(11) default NULL,
                          `StatusCode` int(11) default NULL,
                          `IP` varchar(50) default NULL,
                          `CreateTime` datetime default NULL,
                          PRIMARY KEY  (`Id`)
                        ) ENGINE=MyISAM AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;  ");

                    MockMySql(Constr);

                }   
            }  
        }

        private void MockSqlServer(string Constr)
        { 
            using (SqlConnection con = new SqlConnection(Constr))
            {
                for (int i = 0; i < 100; i++)
                { 
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/checklogin','/api/user/checklogin','GET',{new Random().Next(1,999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')"); 
                }

                for (int i = 0; i < 20; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('sms','/user/sendsms','/api/user/sendsms','POST',{new Random().Next(1, 999)},'404','192.168.1.2','{DateTime.Now.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 50; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('payment','/user/pay','/api/user/pay','POST',{new Random().Next(1, 999)},'500','192.168.1.3','{DateTime.Now.AddMinutes(95).ToString("yyyy-MM-dd HH:mm:ss")}')");
                }
            } 
        }

        private void MockMySql(string Constr)
        {
            using (MySqlConnection con = new MySqlConnection(Constr))
            {
                for (int i = 0; i < 100; i++)
                {
                    con.Execute($"Insert Into RequestInfo  (Node,Route,Url,Method,Milliseconds,StatusCode,IP,CreateTime) Values ('auth','/user/checklogin','/api/user/checklogin','GET',{new Random().Next(1, 999)},'500','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 20; i++)
                {
                    con.Execute($"Insert Into RequestInfo (Node,Route,Url,Method,Milliseconds,StatusCode,IP,CreateTime) Values ('sms','/user/sendsms','/api/user/sendsms','POST',{new Random().Next(1, 999)},'404','192.168.1.2','{DateTime.Now.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 50; i++)
                {
                    con.Execute($"Insert Into RequestInfo (Node,Route,Url,Method,Milliseconds,StatusCode,IP,CreateTime) Values ('payment','/user/pay','/api/user/pay','POST',{new Random().Next(1, 999)},'200','192.168.1.3','{DateTime.Now.AddMinutes(95).ToString("yyyy-MM-dd HH:mm:ss")}')");
                }
            }
        } 
    }  
}
