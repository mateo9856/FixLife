using CommunityToolkit.Maui.Views;
using FixLife.ClientApp.Common;
using FixLife.ClientApp.Common.Enums;
using FixLife.ClientApp.Models.Account;
using FixLife.ClientApp.Sessions;
using FixLife.ClientApp.Views.Popups;
using System.Windows.Input;

namespace FixLife.ClientApp.ViewModels.Logon
{
    public class LogonPageViewModel : BaseViewModel
    {
        private string _credentials;
        private string _password;
        public string Credentials { get => _credentials; 
            set 
            {
                _credentials = value;
                OnPropertyChanged();
            } 
        }
        public string Password { get => _password;
            set 
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand LogonCommand { get; private set; }
        public ICommand LogOffCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }

        public LogonPageViewModel()
        {
            LogonCommand = new Command(Logon);
            LogOffCommand = new Command(LogOff);
            RegisterCommand = new Command(Register);
        }

        private async void Logon() {

            var credentials = new
            {
                Credentials = this.Credentials,
                Password = this.Password,
                LoginType = LoginTypeEnum.Normal
            };

            AccountResponseResult response = null;

            try
            {
                using (var client = new WebApiClient<AccountResponseResult>())
                {
                    response = await client.PostPutAsync(credentials, "Account/Login", true);
                }
                if (response != null)
                {
                    UserSession.Token = response.Token;
                    UserSession.Email = response.Email;
                    if (!response.HasPlans.HasValue)
                        throw new Exception("Value HasPlans is null!");
                    if(response.HasPlans.HasValue && !response.HasPlans.Value)
                        await RedirectToPageAsync("//plan/FirstPlanPage");
                    else
                    {
                        NotificationTimer.EnableTiming();
                        await RedirectToPageAsync("//dash/DashboardPage");
                    }
                }
            } catch(Exception ex)
            {
                var errorPopup = new ErrorPopup("Undefined Error", ex.Message);
                await ShowPopup(errorPopup);
            }
            
        }

        private async void Register()
        {
            await RedirectToPageAsync("//login/RegisterPage");
        }

        private async void LogOff() { 
            UserSession.Token = null;
            UserSession.Email = null;
            UserSession.UserPlans = null;
            NotificationTimer.DisableTiming();
            await RedirectToPageAsync("//login/LoginPage");
        }

    }
}
