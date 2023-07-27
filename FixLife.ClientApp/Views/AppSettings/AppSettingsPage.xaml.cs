using FixLife.ClientApp.ViewModels.AppSettings;

namespace FixLife.ClientApp.Views.AppSettings;

public partial class AppSettingsPage : ContentPage
{
	AppSettingsViewModel viewModel;
	public AppSettingsPage(AppSettingsViewModel model)
	{
		BindingContext = (AppSettingsViewModel)model;
		InitializeComponent();
	}

    private void Save_Clicked(object sender, EventArgs e)
    {

    }
}