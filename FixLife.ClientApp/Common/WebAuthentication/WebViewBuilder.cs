namespace FixLife.ClientApp.Common.WebAuthentication
{
    public class WebViewBuilder
    {
        private ContentPage _contentPage;
        private WebView _webView;
        private readonly string _url;

        public WebViewBuilder(string url)
        {
            _url = url;
            this.Create();
        }

        private void Create()
        {
            _webView = new WebView
            {
                Source = _url,
                VerticalOptions = LayoutOptions.Fill
            };

            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            Grid.SetRow(_webView, 0);
            grid.Children.Add(_webView);

            _contentPage = new ContentPage
            {
                Content = grid
            };
        }

        public void Show()
        {
            Application.Current.MainPage.Navigation.PushModalAsync(_contentPage);
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopModalAsync(true);
        }

        public WebView GetWebView()
        {
            return _webView;
        }
    }
}
