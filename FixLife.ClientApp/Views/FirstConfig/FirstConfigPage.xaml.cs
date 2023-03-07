namespace FixLife.ClientApp.Views.FirstConfig;

public partial class FirstConfigPage : ContentPage
{
	public FirstConfigPage()
	{
		InitializeComponent();
	}

    private void onNewPlanClick_Tapped(object sender, TappedEventArgs e)
    {
		Shell.Current.GoToAsync("CreatorPage");
    }
}