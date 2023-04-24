using FixLife.ClientApp.Views.FirstConfig;
using FixLife.ClientApp.Views.MainPage;

namespace FixLife.ClientApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            RegisterRoutes();
            InitializeComponent();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("MainPage", typeof(Views.MainPage.MainPage));
            Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
            Routing.RegisterRoute("FirstPlanPage", typeof(FirstConfigPage));
            Routing.RegisterRoute("CreatorPage", typeof(FirstPlanCreator));
            Routing.RegisterRoute("FirstPlanSummaryPage", typeof(FirstPlanSummary));
            Routing.RegisterRoute("DashboardPage", typeof(Dashboard));
        }

    }
}