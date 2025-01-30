using FixLife.ClientApp.ViewModels.FirstConfig;

namespace FixLife.ClientApp.Views.FirstConfig.PlanViews;

public partial class FreeTime : ContentView
{
	public FreeTime()
	{
        BindingContext = Application.Current.Handler.MauiContext.Services.GetService<FreeTimeViewModel>();
        InitializeComponent();
	}

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        
        if(sender is Entry entry && !string.IsNullOrEmpty(e.NewTextValue))
        {
            if (!e.NewTextValue.All(char.IsDigit))
                entry.Text = e.OldTextValue;
        }
    }
}