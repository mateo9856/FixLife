namespace FixLife.ClientApp.Views.FirstConfig;

public partial class FirstPlanSummary : ContentPage
{
	public FirstPlanSummary()
	{
		InitializeComponent();
	}

    private async Task CorrectData(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("//plan/CreatorPage");
    }
}