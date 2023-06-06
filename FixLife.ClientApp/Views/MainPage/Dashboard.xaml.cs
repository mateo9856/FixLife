using FixLife.ClientApp.ViewModels.Main;

namespace FixLife.ClientApp.Views.MainPage;

public partial class Dashboard : ContentPage
{
	public Dashboard(DashboardViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}