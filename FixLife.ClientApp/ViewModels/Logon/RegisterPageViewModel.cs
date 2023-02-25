using FixLife.ClientApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixLife.ClientApp.ViewModels.Logon
{
    public class RegisterPageViewModel : BaseViewModel
    {
        public ICommand RegisterCommand { get; private set; }

        private string _email;
        public string Email { get => _email;
            set 
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        private string _password;
        public string Password { get => _password; 
            set 
            {
                _password = value;
                OnPropertyChanged();
            } 
        }
        private string _retypePassword;
        public string RetypePassword
        {
            get => _retypePassword;
            set
            {
                _retypePassword = value;
                OnPropertyChanged();
            }
        }
        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public RegisterPageViewModel() 
        { 
            RegisterCommand = new Command(Register);
        }

        private void Register()
        {
            var registerRequest = new
            {
                Email = Email,
                Password = Password,
                PhoneNumber = PhoneNumber
            };

            string response = null;

            using(var client = new WebApiClient())
            {
                response = client.PostPutAsync(registerRequest, "Account/Register", true).Result;
            }

            if(response != null)
                RedirectToPageAsync("MainPage");

        }

    }
}
