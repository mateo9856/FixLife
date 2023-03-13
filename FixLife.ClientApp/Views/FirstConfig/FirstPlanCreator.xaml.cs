using FixLife.ClientApp.ViewModels.FirstConfig;

namespace FixLife.ClientApp.Views.FirstConfig;

public partial class FirstPlanCreator : ContentPage
{
	public FirstPlanCreator()
	{
		InitializeComponent();
	}

    private void Button_Clicked_Next(object sender, EventArgs e)
    {
        var model = BindingContext as FirstPlanViewModel;
        model.ClickNext.Invoke((Button)sender);
        //if (WeeklyWork.IsEnabled)
        //{
        //    FreeTime.IsEnabled= true;
        //    FreeTime.IsVisible= true;
        //    WeeklyWork.IsEnabled = false;
        //    WeeklyWork.IsVisible = false;
        //}
        //else if(FreeTime.IsEnabled)
        //{
        //    LearnTime.IsEnabled= true;
        //    LearnTime.IsVisible= true;
        //    FreeTime.IsEnabled= false;
        //    FreeTime.IsVisible= false;
        //}
        //else if(LearnTime.IsEnabled)
        //{
        //    //TODO: Redirect to summary
        //}
    }

    private void Button_Clicked_Previous(object sender, EventArgs e)
    {
        var model = BindingContext as FirstPlanViewModel;
        model.ClickPrev.Invoke((Button)sender);
        //if (FreeTime.IsEnabled)
        //{
        //    FreeTime.IsEnabled = false;
        //    FreeTime.IsVisible= false;
        //    WeeklyWork.IsEnabled = true;
        //    WeeklyWork.IsVisible = true;
        //}
        //else if (LearnTime.IsEnabled)
        //{
        //    LearnTime.IsEnabled = false;
        //    LearnTime.IsVisible = false;
        //    FreeTime.IsEnabled = true;
        //    FreeTime.IsVisible= true;
        //}

        //if(WeeklyWork.IsEnabled)
        //{
        //    var btn = sender as Button;
        //    btn.IsEnabled = false;
        //}
        //else
        //{
        //    var btn = sender as Button;
        //    btn.IsEnabled = true;
        //}

    }
}