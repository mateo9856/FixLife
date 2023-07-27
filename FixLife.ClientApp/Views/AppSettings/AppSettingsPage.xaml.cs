using FixLife.ClientApp.ViewModels.AppSettings;

namespace FixLife.ClientApp.Views.AppSettings;

public partial class AppSettingsPage : ContentPage
{
	public AppSettingsPage()
	{
		BindingContext = new AppSettingsViewModel();
		InitializeComponent();
	}

    private void Save_Clicked(object sender, EventArgs e)
    {

    }
}