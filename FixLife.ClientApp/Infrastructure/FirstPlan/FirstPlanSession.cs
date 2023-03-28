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
        
        //TODO: Prepare session persister to summary

        public static FirstPlanSession Instance()
        {
            if(_instance == null)
                _instance = new FirstPlanSession();
            return _instance;
        }
        
    }
}
