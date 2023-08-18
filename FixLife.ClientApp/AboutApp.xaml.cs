namespace FixLife.ClientApp;

public partial class AboutApp : ContentPage
{
	public string FixLifeVersion { get; set; }
	public AboutApp()
	{
		FixLifeVersion = "0.1";
		BindingContext = this;
		InitializeComponent();
	}
}