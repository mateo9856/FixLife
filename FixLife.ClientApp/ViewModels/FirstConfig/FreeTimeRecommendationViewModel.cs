using System.Collections.ObjectModel;

namespace FixLife.ClientApp.ViewModels.FirstConfig
{
    public class FreeTimeRecommendationViewModel : BasePopupViewModel
    {
        public ObservableCollection<string> FreeTimes { get; set; }
        public FreeTimeRecommendationViewModel() { 
            
        }
    }
}
