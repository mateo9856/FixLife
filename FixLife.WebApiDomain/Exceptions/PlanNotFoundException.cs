using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiDomain.Exceptions
{
    public class PlanNotFoundException : Exception
    {
        public PlanNotFoundException(string message) : base(message) 
        { }
    }
}
