using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixLife.ClientApp.ViewModels.Main
{
    public class DashboardViewModel : BaseViewModel
    {
        public ICommand EditPlanCommand { get; private set; }

        public DashboardViewModel()
        {
            EditPlanCommand = new Command(async e => await EditPlan());
        }

        private async Task EditPlan()
        {
            
        }

    }
}
