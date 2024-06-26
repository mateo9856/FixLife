﻿using FixLife.ClientApp.Common;
using FixLife.ClientApp.Models.Account;
using System.Windows.Input;

namespace FixLife.ClientApp.ViewModels.Logon
{
    public class RegisterPageViewModel : BaseViewModel
    {
        private readonly WebApiClient<AccountResponseResult> _webApiClient;
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

        public RegisterPageViewModel(WebApiClient<AccountResponseResult> webApiClient) 
        {
            _webApiClient = webApiClient;
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
            response = await _webApiClient.PostPutAsync(registerRequest, "Account/Register", true);

            if (response != null)
                await RedirectToPageAsync("//login/LoginPage");

        }

    }
}
