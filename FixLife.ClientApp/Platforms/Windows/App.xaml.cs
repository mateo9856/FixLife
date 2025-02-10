namespace FixLife.ClientApp.WinUI
{
    public partial class App : MauiWinUIApplication
    {
        public App()
        {
            if (WinUIEx.WebAuthenticator.CheckOAuthRedirectionActivation())
                return;
            this.InitializeComponent();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}