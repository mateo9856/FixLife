using FixLife.ClientApp.Models.Account;

namespace FixLife.ClientApp.Common.Abstraction
{
    public interface IWebAuthenticateService
    {
        string SelectedClient { get; set; }
        void CloseWebView();
        string LoadOAuthUri(string client);
        Task AuthenticateAsync(string client);
        string GetToken();
        Task<AccountResponseResult> LogonByOAuthToken();
        void AddTokenToSession(string token, string email);
    }
}
