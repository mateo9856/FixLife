namespace FixLife.ClientApp.Common.Abstraction
{
    public interface IWebAuthenticateService
    {
        string SelectedClient { get; set; }
        void CloseWebView();
        string LoadOAuthUri(string client);
        Task AuthenticateAsync(string client);
        string GetToken();
    }
}
