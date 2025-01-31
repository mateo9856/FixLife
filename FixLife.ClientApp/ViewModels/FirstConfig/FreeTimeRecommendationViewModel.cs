using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FixLife.ClientApp.ViewModels.FirstConfig
{
    public class FreeTimeRecommendationViewModel : BasePopupViewModel
    {
        public ObservableCollection<string> FreeTimes { get; set; }
        
        public ICommand SelectedRecommendationCommand { get; set; }

        public string SelectedRecommendation { get; set; }

        public FreeTimeRecommendationViewModel() { 

            FreeTimes = new ObservableCollection<string>(); 
            SelectedRecommendationCommand = new Command<string>(OnSelectedRecommendation);
        }

        private void OnSelectedRecommendation(string recommendation)
        {
            SelectedRecommendation = recommendation;
        }

    }
}
