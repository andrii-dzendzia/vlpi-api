using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vlpi.Domain.Dto
{
    public class Response
    {
        public string Status { get; set; }
        public dynamic Data { get; set; }
        public string Message { get; set; }
        public string ErrorType { get; set; }
    }
}
