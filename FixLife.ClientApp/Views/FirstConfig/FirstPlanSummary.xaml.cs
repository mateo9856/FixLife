namespace FixLife.ClientApp.Views.FirstConfig;

public partial class FirstPlanSummary : ContentPage
{
	public FirstPlanSummary()
	{
		InitializeComponent();
	}

    private void CorrectData(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//plan/CreatorPage");
    }
}