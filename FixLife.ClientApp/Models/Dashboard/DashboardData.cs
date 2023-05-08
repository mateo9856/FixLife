using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FixLife.ClientApp.Models;

namespace FixLife.ClientApp.Models.Dashboard
{
    public class DashboardData
    {
        public AppPlan Plan { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
    }
}
