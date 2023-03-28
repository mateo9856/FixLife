namespace FixLife.ClientApp.Views.FirstConfig.PlanViews;

public partial class LearnTime : ContentView
{
	public LearnTime()
	{
		InitializeComponent();
	}

    private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {

    }

    private void TimePicker_Focused(object sender, FocusEventArgs e)
    {
        var el = (TimePicker)sender;
        el.BackgroundColor = new Color(255, 255, 255);
        el.TextColor = new Color(0, 0, 0);
    }

    private void TimePicker_Unfocused(object sender, FocusEventArgs e)
    {
        var el = (TimePicker)sender;
        el.BackgroundColor = new Color(0, 0, 0);
        el.TextColor = new Color(255, 255, 255);
    }
}