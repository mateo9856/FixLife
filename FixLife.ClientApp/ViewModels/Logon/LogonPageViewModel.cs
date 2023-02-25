using FixLife.ClientApp.Common;
using FixLife.ClientApp.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void Logon() {

            var credentials = new
            {
                Credentials = this.Credentials,
                Password = this.Password,
                LoginType = LoginTypeEnum.Normal
            };

            string response = null;

            using (var client = new WebApiClient())
            {
                response = client.PostPutAsync(credentials, "Account/Login", true).Result;
            }
            if(response != null)
                RedirectToPageAsync("MainPage");
        }

        private void Register()
        {
            RedirectToPageAsync("RegisterPage");
        }

        private void LogOff() { 
        
        }

    }
}
