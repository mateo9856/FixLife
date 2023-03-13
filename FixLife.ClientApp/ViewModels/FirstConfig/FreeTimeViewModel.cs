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
        
        public Action<Button> HobbysList { get; set; }

        public FreeTimeViewModel()
        {
            HobbysList = ShowHobbysList;
        }

        private void ShowHobbysList(Button btn)
        {
            //TODO: Show List
        }

    }
}
