using FixLife.ClientApp.Models.AppPlan;
using FixLife.ClientApp.ViewModels.FirstConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Infrastructure.FirstPlan
{
    public sealed class FirstPlanSession
    {
        private static FirstPlanSession _instance;
        private FirstPlanSession() { }
        public AppPlan SummaryPlan { get; set; }

        public static FirstPlanSession Instance()
        {
            if(_instance == null)
                _instance = new FirstPlanSession();
            return _instance;
        }
        
        public void Dispose() => _instance = null;

    }
}
