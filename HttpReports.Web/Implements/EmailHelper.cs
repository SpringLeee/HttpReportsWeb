using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HttpReports.Web.Implements
{
    public static class EmailHelper
    { 
        public static void Send(string to, string title, string content)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var url = "http://email.soboob.com/api/email/send";

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = httpClient.PostAsync(url,new StringContent(JsonConvert.SerializeObject(new {  
                    to,title,content 
                }), System.Text.Encoding.UTF8, "application/json")).Result;

                string result = response.Content.ReadAsStringAsync().Result;
            }
        }  

    }
}
