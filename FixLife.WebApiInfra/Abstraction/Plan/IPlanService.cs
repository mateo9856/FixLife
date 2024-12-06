﻿using FixLife.WebApiDomain.Models;
using FixLife.WebApiDomain.Plan;

namespace FixLife.WebApiInfra.Abstraction
{
    public interface IPlanService
    {
        Task<(short, string)> CreatePlanAsync(PlanModel planModel, bool isFirst, string userId);
        Task AssignPlanToUserAsync(string userId, Plan plan);
        Task<(short, string)> GetPlanIdAsync(string userId);
        Task<(short, string)> EditPlanAsync(PlanModel planModel, PlanModel oldPlanModel, string userId);
        Task<Plan?> GetPlanAsync(string userId);
    }
}
