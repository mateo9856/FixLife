﻿using System;
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

        public LogonPageViewModel()
        {
            LogonCommand = new Command(Logon);
            LogOffCommand = new Command(LogOff);
        }

        private void Logon() {
            var log = Credentials;
            var pas = Password;
            //TODO implement logon service
            RedirectToPageAsync("MainPage");
        }

        private void LogOff() { 
        
        }

    }
}
