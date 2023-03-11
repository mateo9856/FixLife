using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.ViewModels.FirstConfig
{
    public class FreeTimeViewModel : BaseViewModel
    {
        private string _hobbyText;
        public string HobbyText
        {
            get { return _hobbyText; }
            set { _hobbyText = value; OnPropertyChanged(); }
        }

        //TODO: Think how to change controls data by viewModel
        //1st think, event handler
    }
}
