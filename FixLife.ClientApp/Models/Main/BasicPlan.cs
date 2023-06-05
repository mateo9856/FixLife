using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Models.Main
{
    public class BasicPlan
    {
        public string WeeklyWorkDesc { get; set; }
        public IEnumerable<string> FreeTimes { get; set; }
        public string LearnTime { get; set; }
    }
}
