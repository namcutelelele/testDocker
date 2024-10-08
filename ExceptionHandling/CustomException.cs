using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling
{
    public class CustomException : Exception
    {
        public HttpStatusCode Status { get; private set; }
        public string Error { get; set; }
        public List<object> Data { get; set; }

        public CustomException(HttpStatusCode status, string msg, string error, List<object> data) : base(msg)
        {
            Status = status;
            Error = error;
            Data = data;
        }

    }
}
