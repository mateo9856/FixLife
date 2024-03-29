﻿using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task RedirectToPageAsync(string page) {
            await Shell.Current.GoToAsync(page);
        }

        protected async Task ShowPopup(Popup popup)
        {
            var page = Application.Current?.MainPage;
            if (page != null)
                await page.ShowPopupAsync(popup);
        }

    }
}
