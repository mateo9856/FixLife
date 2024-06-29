namespace FixLife.ClientApp.Common.Abstraction
{
    public interface IWebAuthenticateService
    {
        Task AuthenticateAsync(string client);
        string GetToken();
    }
}
