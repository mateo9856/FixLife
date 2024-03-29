﻿using FixLife.WebApiDomain.Plan;
using FixLife.WebApiInfra.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiInfra.Abstraction
{
    public interface IPlanService
    {
        Task<(short, string)> CreatePlanAsync(Plan plan, bool isFirst, string userId);
        Task AssignPlanToUserAsync(string userId, Plan plan);
        Task<(short, string)> GetPlanIdAsync(string userId);
        Task<(short, string)> EditPlanAsync(Plan plan, Plan oldPlan, string userId);
        Task<Plan?> GetPlanAsync(string userId);
    }
}
