using CommunityToolkit.Maui.Views;

namespace FixLife.ClientApp.Resources.Popups;

public partial class ErrorPopup : Popup
{
	public ErrorPopup(string text = null)
	{
		InitializeComponent();
		if(text != null) 
			ErrorText.Text = text;
	}

    private void Button_Closed(object sender, EventArgs e)
    {
		this.Close();
    }
}