using CommunityToolkit.Maui.Views;

namespace FixLife.ClientApp.Views.Popups;

public partial class ErrorPopup : Popup
{
	public string Id { get; set; }
	public string Message { get; set; }
	public ErrorPopup(string id, string message)
	{
		BindingContext= this;
		Id = id;
		Message = message;
		InitializeComponent();
	}
}