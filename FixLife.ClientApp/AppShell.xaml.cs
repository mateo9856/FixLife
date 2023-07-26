using FixLife.ClientApp.Sessions;
using FixLife.ClientApp.Views.AppSettings;
using FixLife.ClientApp.Views.FirstConfig;
using FixLife.ClientApp.Views.MainPage;

namespace FixLife.ClientApp
{
    public partial class AppShell : Shell
    {
        public bool IsLogged { get => !string.IsNullOrEmpty(UserSession.Token); }
        public AppShell()
        {
            BindingContext = this;
            RegisterRoutes();
            InitializeComponent();
        }

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);
            var orString = args.Target.Location.OriginalString;
            if (!IsLogged && (orString.Contains("DashboardPage") || orString.Contains("FirstPlanPage")))
            {
                args.Cancel();
            }
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("MainPage", typeof(Views.MainPage.MainPage));
            Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
            Routing.RegisterRoute("FirstPlanPage", typeof(FirstConfigPage));
            Routing.RegisterRoute("CreatorPage", typeof(FirstPlanCreator));
            Routing.RegisterRoute("FirstPlanSummaryPage", typeof(FirstPlanSummary));
            Routing.RegisterRoute("DashboardPage", typeof(Dashboard));
            Routing.RegisterRoute("AppSettingsPage", typeof(AppSettingsPage));
        }

    }
}