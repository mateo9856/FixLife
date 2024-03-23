using FixLife.ClientApp.ViewModels.FirstConfig;

namespace FixLife.ClientApp.Views.FirstConfig;

public partial class FirstPlanSummary : ContentPage
{
	public FirstPlanSummary(FirstPlanSummaryViewModel model)
	{
		BindingContext = model;
		InitializeComponent();
	}

    private void CorrectData(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//plan/CreatorPage");
    }
}