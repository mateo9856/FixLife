using FixLife.ClientApp.Models.AppPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixLife.ClientApp.ViewModels.FirstConfig
{
    public class FirstPlanSummaryViewModel
    {
        public AppPlan SummaryPlan { get; set; }
        public ICommand CreateCommand { get; private set; }
        public FirstPlanSummaryViewModel()
        {
            CreateCommand = new Command(async () => await Create());
        }

        private async Task Create()
        {

        }

    }
}
