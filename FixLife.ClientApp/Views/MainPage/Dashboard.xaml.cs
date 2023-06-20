using FixLife.ClientApp.ViewModels;

namespace FixLife.ClientApp.Views.MainPage;

public partial class Dashboard : ContentPage
{
	public Dashboard(DashboardViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}