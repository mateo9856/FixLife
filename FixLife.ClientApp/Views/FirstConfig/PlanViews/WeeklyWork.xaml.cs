using FixLife.ClientApp.ViewModels.FirstConfig;

namespace FixLife.ClientApp.Views.FirstConfig.PlanViews;

public partial class WeeklyWork : ContentView
{
    private FirstPlanViewModel viewModel { get; set; }
	public WeeklyWork()
	{
		InitializeComponent();
        viewModel = (FirstPlanViewModel)BindingContext;
	}

    private void SetPreviousImage(object sender, TappedEventArgs e)
    {
        viewModel.SetImage("Left", activeWorkMessage);
    }

    private void SetNextImage(object sender, TappedEventArgs e)
    {
        viewModel.SetImage("Right", activeWorkMessage);
    }
}