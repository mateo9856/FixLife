namespace FixLife.ClientApp.Common.Abstraction
{
    public interface IWebAuthenticateService
    {
        string SelectedClient { get; set; }
        void RunWebView(string uri);
        void CloseWebView();
        Task<string> LoadOAuthUri(string client);
        Task AuthenticateAsync(string client);
        string GetToken();
    }
}
