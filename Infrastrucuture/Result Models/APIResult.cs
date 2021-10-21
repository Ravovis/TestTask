using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastrucuture.Result_Models
{
    public class APIResult<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
