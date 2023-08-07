﻿using FixLife.WebApiDomain.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiInfra.Abstraction.Dashboard
{
    public interface IDashboardService
    {
        Task<(short, Plan)> GetDashboardData(string user);
        Task<(string, string)> HandleDetectPush(string user);
    }
}
