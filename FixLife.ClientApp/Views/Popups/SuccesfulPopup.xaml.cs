using CommunityToolkit.Maui.Views;

namespace FixLife.ClientApp.Views.Popups;

public partial class SuccesfulPopup : Popup
{
	public SuccesfulPopup()
	{
		InitializeComponent();
	}
    private void Button_Close(object sender, EventArgs e) => Close();
}