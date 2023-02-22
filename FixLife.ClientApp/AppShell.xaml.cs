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
        }

    }
}