using FixLife.ClientApp.ViewModels;

namespace FixLife.ClientApp.Views.MainPage;

public partial class Dashboard : ContentPage
{
	public Dashboard(DashboardViewModel vm)
	{
		//TODO: Style xaml and prepare notifications and edit
		BindingContext = vm;
		InitializeComponent();
	}
}