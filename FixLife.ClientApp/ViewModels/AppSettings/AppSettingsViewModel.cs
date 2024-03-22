using CommunityToolkit.Maui.Views;
using FixLife.ClientApp.Common;
using FixLife.ClientApp.Options;
using FixLife.ClientApp.Views.Popups;
using Microsoft.Extensions.Configuration;
using System.Windows.Input;

namespace FixLife.ClientApp.ViewModels.AppSettings
{
    public class AppSettingsViewModel : BaseViewModel
    {
        private readonly IConfiguration _configuration;
        private ClientOptions _options;

        private bool _notificationEnabled;
        public bool NotificationEnabled
        {
            get => _notificationEnabled;
            set
            {
                _notificationEnabled = value;
                OnPropertyChanged();
            }
        }
        private bool _oldPlansToFileEnabled;
        public bool OldPlansToFileEnabled
        {
            get => _oldPlansToFileEnabled;
            set
            {
                _oldPlansToFileEnabled = value;
                OnPropertyChanged();
            }
        }
        private bool _appTheme;
        public bool LightTheme
        {
            get => _appTheme;
            set
            {
                _appTheme = value;
                OnPropertyChanged();
            }
        }
        private bool _shareEnabled;
        public bool ShareEnabled
        {
            get => _shareEnabled;
            set
            {
                _shareEnabled = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; private set; }

        public AppSettingsViewModel(IConfiguration configuration)
        {
            _configuration = configuration;
            _options = SettingsOperations.LoadToOptionsPage();
            NotificationEnabled = _options.NotificationEnabled;
            OldPlansToFileEnabled = _options.OldPlansToFileEnabled;
            LightTheme = _options.LightTheme;
            ShareEnabled = _options.ShareEnabled;
            SaveCommand = new Command(async () => await SaveData());
        }

        private async Task SaveData()
        {
            _options = new()
            {
                LightTheme = LightTheme,
                ShareEnabled = ShareEnabled,
                OldPlansToFileEnabled = OldPlansToFileEnabled,
                NotificationEnabled = NotificationEnabled,
            };

            var setResult = SettingsOperations.SaveNewSettings(_options);

            Popup popup = null;
            if(setResult) 
            {
                popup = new SuccesfulPopup();
            } else
            {
                popup = new ErrorPopup("500", "Application settings change error!");
            }
            await ShowPopup(popup);
        }
    }
}
