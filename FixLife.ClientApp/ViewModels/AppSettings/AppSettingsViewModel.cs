﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixLife.ClientApp.ViewModels.AppSettings
{
    public class AppSettingsViewModel : BaseViewModel
    {
        private readonly IConfiguration _configuration;
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
        public bool AppTheme
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
            NotificationEnabled = false;
            OldPlansToFileEnabled = false;
            AppTheme = false;
            ShareEnabled = false;
            SaveCommand = new Command(async () => await SaveData());
        }

        private async Task SaveData()
        {
            
        }
    }
}
