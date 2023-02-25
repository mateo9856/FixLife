using FixLife.ClientApp.Common;
using FixLife.ClientApp.Models.Account;
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

        private async void Register()
        {
            var registerRequest = new
            {
                Email = Email,
                Password = Password,
                PhoneNumber = PhoneNumber
            };

            object response = null;

            using(var client = new WebApiClient<AccountResponseResult>())
            {
                response = await client.PostPutAsync(registerRequest, "Account/Register", true);
            }

            if(response != null)
                await RedirectToPageAsync("MainPage");

        }

    }
}
