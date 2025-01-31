using FixLife.ClientApp.ViewModels.FirstConfig;

namespace FixLife.ClientApp.Views.FirstConfig.PlanViews;

public partial class FreeTime : ContentView
{
	public FreeTime()
	{
        BindingContext = Application.Current.Handler.MauiContext.Services.GetService<FreeTimeViewModel>();
        InitializeComponent();
	}

}