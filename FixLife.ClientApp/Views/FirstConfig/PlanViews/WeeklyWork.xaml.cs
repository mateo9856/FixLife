using FixLife.ClientApp.ViewModels.FirstConfig;

namespace FixLife.ClientApp.Views.FirstConfig.PlanViews;

public partial class WeeklyWork : ContentView
{
    private WeeklyWorkViewModel viewModel { get; set; }
	public WeeklyWork()
	{
		InitializeComponent();
        viewModel = (WeeklyWorkViewModel)BindingContext;
	}

    private void SetPreviousImage(object sender, TappedEventArgs e)
    {
        viewModel.SetImage("Left", activeWorkMessage, SetterText);
    }

    private void SetNextImage(object sender, TappedEventArgs e)
    {
        viewModel.SetImage("Right", activeWorkMessage, SetterText);
    }
}