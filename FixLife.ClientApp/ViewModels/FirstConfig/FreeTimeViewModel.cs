using CommunityToolkit.Maui.Views;
using FixLife.ClientApp.Common.Abstraction;
using FixLife.ClientApp.Models.FirstPlan;
using FixLife.ClientApp.Views.Popups;
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

        private string _suggestCount;
        public string SuggestCount
        {
            get => _suggestCount;
            set
            {
                _suggestCount = value; OnPropertyChanged();
            }
        }

        public ObservableCollection<FreeTimeListItem> FreeTimeListItems { get; set; }

        public FreeTimeRecommendationViewModel RecommendationViewModel { get; set; }

        public ICommand AddToListCommand { get; set; }

        public ICommand SuggestCommand { get; set; }
        public FreeTimeViewModel(IPlanRecommendationService recommendationService)
        {
            _recommenationService = recommendationService;
            FreeTimeListItems = new ObservableCollection<FreeTimeListItem>();
            AddToListCommand = new Command(AddToList);
            SuggestCommand = new Command(async() => await SuggestByGemini());
        }

        private async Task SuggestByGemini()
        {
            var suggestCountToInt = int.TryParse(SuggestCount, out int suggCount) ? suggCount : 0;

            if (suggestCountToInt <= 0)
                return;

            var result = await _recommenationService.GetFreeTimeRecommendationAsync(suggCount);

            if (result.FreeTimes.Count <= 0 || result is null) {
                var errorPopup = new ErrorPopup("404", "Call error or Not Found!");
                await ShowPopup(errorPopup);
                return;
            }

            Popup popup;

            RecommendationViewModel = new FreeTimeRecommendationViewModel();

            RecommendationViewModel.FreeTimes = new ObservableCollection<string>(result.FreeTimes);
            popup = new FreeTimeRecommendationPopup(RecommendationViewModel);

            RecommendationViewModel.RecommendationSelected += (sender, e) =>
            {
                HobbyText = e;
                AddToList();
                RecommendationViewModel.ClosePopup(popup);
            };
            await ShowPopup(popup);

        }

        private void AddToList()
        {
            var element = new FreeTimeListItem {Name = HobbyText, TimeEnd = FreeTimeEnd, TimeStart = FreeTimeStart};
            FreeTimeListItems.Add(element);
        }

    }
}
