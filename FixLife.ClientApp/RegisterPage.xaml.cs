using FixLife.ClientApp.ViewModels.Logon;

namespace FixLife.ClientApp;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterPageViewModel model)
	{
		BindingContext = model;
		InitializeComponent();
	}
}