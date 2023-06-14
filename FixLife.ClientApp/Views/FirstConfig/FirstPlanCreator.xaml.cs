using FixLife.ClientApp.ViewModels.FirstConfig;

namespace FixLife.ClientApp.Views.FirstConfig;

public partial class FirstPlanCreator : ContentPage
{
    private FirstPlanViewModel context { get; set; }
    private string ActiveView { get; set; }
	public FirstPlanCreator()
	{
		InitializeComponent();
        ActiveView = "WeeklyWork";
        context = (FirstPlanViewModel)BindingContext;
	}

    private async void Button_Clicked_Next(object sender, EventArgs e)
    {
        if (WeeklyWork.IsEnabled)
        {
            context.SetModel(WeeklyWork.BindingContext);
            ActiveView = "FreeTime";
            FreeTime.IsEnabled= true;
            FreeTime.IsVisible= true;
            WeeklyWork.IsEnabled = false;
            WeeklyWork.IsVisible = false;
            btnPrev.IsEnabled = true;
        }
        else if(FreeTime.IsEnabled)
        {
            context.SetModel(FreeTime.BindingContext);
            ActiveView = "LearnTime";
            LearnTime.IsEnabled= true;
            LearnTime.IsVisible= true;
            FreeTime.IsEnabled= false;
            FreeTime.IsVisible= false;
        }
        else if(LearnTime.IsEnabled)
        {
            context.SetModel(LearnTime.BindingContext);
            context.SummaryPlan();
            await Shell.Current.GoToAsync("FirstPlanSummaryPage");
        }
    }

    private void Button_Clicked_Previous(object sender, EventArgs e)
    {
        if (FreeTime.IsEnabled)
        {
            ActiveView = "WeeklyWork";
            FreeTime.IsEnabled = false;
            FreeTime.IsVisible= false;
            WeeklyWork.IsEnabled = true;
            WeeklyWork.IsVisible = true;
        }
        else if (LearnTime.IsEnabled)
        {
            ActiveView = "FreeTime";
            LearnTime.IsEnabled = false;
            LearnTime.IsVisible = false;
            FreeTime.IsEnabled = true;
            FreeTime.IsVisible= true;
        }

        if(WeeklyWork.IsEnabled)
        {
            var btn = sender as Button;
            btn.IsEnabled = false;
        }
        else
        {
            var btn = sender as Button;
            btn.IsEnabled = true;
        }

    }
}