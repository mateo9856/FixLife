using FixLife.ClientApp.Common.Abstraction;
using FixLife.ClientApp.Models.FirstPlan;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FixLife.ClientApp.ViewModels.FirstConfig
{
    public class FreeTimeViewModel : BaseViewModel
    {
        private readonly IPlanRecommendationService _recommenationService;

        private TimeSpan _freeTimeStart;
        private TimeSpan _freeTimeEnd;
        public TimeSpan FreeTimeStart
        {
            get
            {
                return _freeTimeStart;
            }
            set
            {
                _freeTimeStart = value;
                OnPropertyChanged();
            }
        }
        public TimeSpan FreeTimeEnd
        {
            get
            {
                return _freeTimeEnd;
            }
            set
            {
                _freeTimeEnd = value;
                OnPropertyChanged();
            }
        }

        private string _hobbyText;
        public string HobbyText
        {
            get { return _hobbyText; }
            set { _hobbyText = value; OnPropertyChanged(); }
        }
        
        public Action<Button> HobbysList { get; set; }

        public ObservableCollection<FreeTimeListItem> FreeTimeListItems { get; set; }

        public ICommand AddToListCommand { get; set; }

        public ICommand SuggestCommand { get; set; }
        public FreeTimeViewModel(IPlanRecommendationService recommendationService)
        {
            _recommenationService = recommendationService;
            FreeTimeListItems = new ObservableCollection<FreeTimeListItem>();
            AddToListCommand = new Command(async () => await AddToList());
            SuggestCommand = new Command(async() => await SuggestByGemini());
            HobbysList = ShowHobbysList;
        }

        private void ShowHobbysList(Button btn)
        {
            //TODO: Show List
        }

        private async Task SuggestByGemini()
        {
            var result = await _recommenationService.GetFreeTimeRecommendationAsync();
        }

        private async Task AddToList()
        {
            var element = new FreeTimeListItem {Name = HobbyText, TimeEnd = FreeTimeEnd, TimeStart = FreeTimeStart};
            FreeTimeListItems.Add(element);
        }

    }
}
