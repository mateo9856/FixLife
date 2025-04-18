using FixLife.ClientApp.Common;
using FixLife.ClientApp.Common.Abstraction;
using FixLife.ClientApp.Common.Enums;
using FixLife.ClientApp.Models.Account;
using FixLife.ClientApp.Sessions;
using FixLife.ClientApp.Views.Popups;
using System.Windows.Input;

namespace FixLife.ClientApp.ViewModels.Logon
{
    public class LogonPageViewModel : BaseViewModel
    {
        private readonly WebApiClient<AccountResponseResult> _webApiClient;
        private readonly IWebAuthenticateService _webAuthenticationService;

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
        public ICommand GoogleLogonCommand { get; private set; }
        public ICommand FacebookLogonCommand { get; private set; }
        public ICommand AppleLogonCommand { get; private set; }
        public LogonPageViewModel(WebApiClient<AccountResponseResult> webApiClient, IWebAuthenticateService webAuthenticateService)
        {
            _webApiClient = webApiClient;
            _webAuthenticationService = webAuthenticateService;
            LogonCommand = new Command(Logon);
            LogOffCommand = new Command(LogOff);
            RegisterCommand = new Command(Register);
            GoogleLogonCommand = new Command(() =>
            {
                OAuthLogon("Google");
            });
            FacebookLogonCommand = new Command(() =>
            {
                OAuthLogon("Facebook");
            });
            AppleLogonCommand = new Command(() =>
            {
                OAuthLogon("Apple");
            });
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
                response = await _webApiClient.PostPutAsync(credentials, "Account/Login", true);
                if (response != null)
                    AssignLoginValuesAndRedirect(response);

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

        private async void OAuthLogon(string oAuthClient)
        {
            _webAuthenticationService.SelectedClient = oAuthClient;
            _webAuthenticationService.LoadOAuthUri(oAuthClient);
            await _webAuthenticationService.AuthenticateAsync(oAuthClient);
            
            try
            {
                var response = await _webAuthenticationService.LogonByOAuthToken();
                if (response != null)
                    AssignLoginValuesAndRedirect(response);

            } catch (Exception ex)
            {
                var errorPopup = new ErrorPopup("Undefined Error", ex.Message);
                await ShowPopup(errorPopup);
            }
        }

        private async void AssignLoginValuesAndRedirect(AccountResponseResult loginResponse)
        {
            UserSession.Token = loginResponse.Token;
            UserSession.Email = loginResponse.Email;
            if (!loginResponse.HasPlans.HasValue)
                throw new Exception("Value HasPlans is null!");
            if (loginResponse.HasPlans.HasValue && !loginResponse.HasPlans.Value)
                await RedirectToPageAsync("//plan/FirstPlanPage");
            else
            {
                NotificationTimer.EnableTiming();
                await RedirectToPageAsync("//dash/DashboardPage");
            }
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
