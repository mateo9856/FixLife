using FixLife.ClientApp.Views.FirstConfig;

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
        }

    }
}