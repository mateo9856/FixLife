using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixLife.ClientApp.ViewModels.FirstConfig
{
    public class FirstPlanViewModel : BaseViewModel
    {
        public ICommand NextCommand { get; private set; }

        public FirstPlanViewModel()
        {
            NextCommand = new Command(async () =>
            {
                await Next();
            });
        }

        private async Task Next()
        {
            //TODO: Prepare set to next view
        }

    }
}
