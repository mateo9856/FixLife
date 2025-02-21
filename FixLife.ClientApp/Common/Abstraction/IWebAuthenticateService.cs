namespace FixLife.ClientApp.Common.Abstraction
{
    public interface IWebAuthenticateService
    {
        string SelectedClient { get; set; }
        Task AuthenticateAsync(string client);
        string GetToken();
    }
}
