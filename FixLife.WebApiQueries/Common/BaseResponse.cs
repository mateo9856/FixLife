using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.Common
{
    public class BaseResponse
    {
        public short Status { get; set; }
        public string Message { get; set; }
    }
}
