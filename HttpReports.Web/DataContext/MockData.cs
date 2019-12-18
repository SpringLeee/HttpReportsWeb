using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HttpReports.Web.DataContext
{
    public class MockData
    {
        public void MockSqlServer(string Constr)
        { 
            using (SqlConnection con = new SqlConnection(Constr))
            {
                for (int i = 0; i < 100; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/aaa','/api/user/aaa','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 98; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/bbb','/api/user/bbb','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 92; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/ccc','/api/user/ccc','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 90; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/ddd','/api/user/ddd','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 82; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/eee','/api/user/eee','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 80; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/fff','/api/user/fff','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 76; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/ggg','/api/user/ggg','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 72; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/iii','/api/user/iii','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 70; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/mmm','/api/user/mmm','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 66; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/ttt','/api/user/ttt','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 65; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/uuu','/api/user/uuu','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 62; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/rrr','/api/user/rrr','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 58; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/vvv','/api/user/vvv','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 30; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/ppp','/api/user/ppp','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 10; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/xxx','/api/user/xxx','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 55; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/lll','/api/user/lll','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 52; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/b1','/api/user/b1','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 53; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/c2','/api/user/c2','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 35; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/h1','/api/user/h1','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 13; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/r1','/api/user/r1','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 31; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/pc','/api/user/pc','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }
                 

                for (int i = 0; i < 30; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/k23','/api/user/k23','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 10; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/mk1','/api/user/mk1','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 55; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/plcm','/api/user/plcm','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 52; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/zhu','/api/user/zhu','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 53; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/umw','/api/user/umw','GET',{new Random().Next(1, 999)},'200','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 35; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('auth','/user/apcod','/api/user/apcod','GET',{new Random().Next(1, 999)},'500','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 13; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('payment','/user/mdfdfd','/api/user/mdfdfd','GET',{new Random().Next(1, 999)},'500','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

                for (int i = 0; i < 31; i++)
                {
                    con.Execute($"Insert Into RequestInfo Values ('log','/user/ddmmm','/api/user/ddmmm','GET',{new Random().Next(1, 999)},'500','192.168.1.1','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                }

            }
        } 

        public void MockMySql(string Constr)
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
