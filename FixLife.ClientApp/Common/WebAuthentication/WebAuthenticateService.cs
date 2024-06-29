using FixLife.ClientApp.Common.Abstraction;

namespace FixLife.ClientApp.Common.WebAuthentication
{
    public class WebAuthenticateService : IWebAuthenticateService
    {
        private string _token = string.Empty;

        public async Task AuthenticateAsync(string client)
        {
            try
            {
                WebAuthenticatorResult authResult = await WebAuthenticator.Default.AuthenticateAsync(
                    new Uri($"https://mysite.com/mobileauth/{client}"),
                    new Uri("fixlife://"));
                string accessToken = authResult?.AccessToken;

                _token = accessToken;
            }
            catch (TaskCanceledException)
            {
                return;
            }
        }

        public string GetToken()
            => _token;
    }
}
