using CommunityToolkit.Maui.Views;
using FixLife.ClientApp.ViewModels.FirstConfig;

namespace FixLife.ClientApp.Views.Popups;

public partial class FreeTimeRecommendationPopup : Popup
{
	public FreeTimeRecommendationPopup(FreeTimeRecommendationViewModel vm)
	{
		BindingContext= vm;
		InitializeComponent();
	}

    private void Button_Close(object sender, EventArgs e)
	{
		((FreeTimeRecommendationViewModel)BindingContext).ClosePopup(this);
	}

}