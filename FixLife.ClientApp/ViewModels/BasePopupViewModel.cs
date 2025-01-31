using CommunityToolkit.Maui.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FixLife.ClientApp.ViewModels
{
    public class BasePopupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void ClosePopup(Popup popup)
        {
            popup.CloseAsync();
        }
    }
}
