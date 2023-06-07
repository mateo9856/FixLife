﻿using FixLife.WebApiDomain.Plan;
using FixLife.WebApiInfra.Abstraction.Dashboard;
using FixLife.WebApiInfra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiInfra.Services.Dashboard
{
    public class DashboardService : BaseService<UserPlan>, IDashboardService
    {
        private readonly IdentityContext _identityContext;
        public DashboardService(ApplicationContext context, IdentityContext identityContext) : base(context)
        {
            _identityContext = identityContext;
        }

        public async Task<(short, Plan)> GetDashboardData(string user)
        {
            var GetPlan = await _context.UserPlan.FirstOrDefaultAsync(d => d.Users.Id == Guid.Parse(user));

            if(GetPlan != null)
            {
                return (200,GetPlan.Plans);
            }

            return (404, null);
        }
    }
}