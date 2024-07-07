using FixLife.ClientApp.Common.Abstraction;
using FixLife.ClientApp.Common.WebAuthentication.Clients;
using Newtonsoft.Json;
using System.Text;

namespace FixLife.ClientApp.Common.WebAuthentication
{
    public class WebAuthenticateService : IWebAuthenticateService
    {
        private string _token = string.Empty;

        public async Task AuthenticateAsync(string client)
        {
            string uri = string.Empty;
            var stateParam = Guid.NewGuid().ToString();
            var shaHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(stateParam));
            var clientData = await ReadDataFromJson(client);
            if (client == "Google")
            {
                uri = @"https://accounts.google.com/o/oauth2/auth?
             scope=email%20profile&
             response_type=code&
             state=" + stateParam + @"&
             redirect_uri=" + clientData.RedirectUri + @"&
             code_challenge=" + shaHash + @"&
             client_id=" + clientData.ClientId;
            }
            if(client == "Facebook")
            {
                uri = string.Concat("https://www.facebook.com/v20.0/dialog/oauth?",
                    $"client_id={clientData.ClientId}",
                    $"&redirect_uri={clientData.RedirectUri}",
                    $"&client_secret={clientData.ClientSecret}",
                    $"&state={stateParam}");
            }
            try
            {
                WebAuthenticatorResult authResult = await WebAuthenticator.Default.AuthenticateAsync(
                    new Uri(uri),
                    new Uri("com.mateo9856.fixlife://"));
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

        private async Task<OAuthClient> ReadDataFromJson(string client)
        {
            using var jsonStream = await FileSystem.OpenAppPackageFileAsync("oauthclients.json");
            using var jsonFile = new StreamReader(jsonStream);
            var jsonText = jsonFile.ReadToEnd();
            var clientsClass = JsonConvert.DeserializeObject<OAuthClientClass>(jsonText);

            return client == "Google" ? clientsClass.Google :
                   client == "Facebook" ? clientsClass.Facebook :
                   throw new Exception("Unhandled OAuth Client");
        }
    }
}
