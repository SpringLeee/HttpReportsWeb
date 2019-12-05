using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpReports.Web.Filters
{
    public class GlobalExceptionFilter: IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            string text = "A class is the most powerful data type in C#. Like a structure, " + "a class defines the data and behavior of the data type. ";

            System.IO.File.WriteAllText(@"C:\Log\1.txt", text);  

        } 
    }
}
