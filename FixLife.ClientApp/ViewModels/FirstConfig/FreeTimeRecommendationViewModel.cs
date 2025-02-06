using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FixLife.ClientApp.ViewModels.FirstConfig
{
    public class FreeTimeRecommendationViewModel : BasePopupViewModel
    {
        public ObservableCollection<string> FreeTimes { get; set; }

        public EventHandler<string> RecommendationSelected;
        public ICommand SelectedRecommendationCommand { get; set; }

        public string SelectedRecommendation { get; set; }

        public FreeTimeRecommendationViewModel() { 

            FreeTimes = new ObservableCollection<string>();
            SelectedRecommendationCommand = new Command((cmd) =>
            {
                OnSelectedRecommendation(cmd.ToString());
            });
        }

        private void OnSelectedRecommendation(string recommendation)
        {
            SelectedRecommendation = recommendation;
            RecommendationSelected?.Invoke(this, SelectedRecommendation);
        }

    }
}
