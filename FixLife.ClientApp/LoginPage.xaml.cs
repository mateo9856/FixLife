using FixLife.ClientApp.ViewModels.Logon;

namespace FixLife.ClientApp;

public partial class LoginPage : ContentPage
{
	public LoginPage(LogonPageViewModel model)
	{
		BindingContext = model;
		InitializeComponent();
	}

}