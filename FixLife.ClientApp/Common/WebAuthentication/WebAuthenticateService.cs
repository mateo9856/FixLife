using FixLife.ClientApp.Common.Abstraction;
using FixLife.ClientApp.Common.Enums;
using FixLife.ClientApp.Common.WebAuthentication.Clients;
using Newtonsoft.Json;
using System.Text;

namespace FixLife.ClientApp.Common.WebAuthentication
{
    public class WebAuthenticateService : IWebAuthenticateService
    {
        private string _token = string.Empty;
        private string _authUri = string.Empty;

        private WebViewBuilder _webViewBuilder;
        private OAuthClient _clientData;
        public string SelectedClient { get; set; }

        public async Task<string> LoadOAuthUri(string client)
        {
            SelectedClient = client;

            var stateParam = Guid.NewGuid().ToString();
            var shaHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(stateParam));
            _clientData = await ReadDataFromJson(client);

                        if (client == "Google")
            {
                _authUri = string.Concat("https://accounts.google.com/o/oauth2/auth?",
                    "scope=email%20profile",
                    "&response_type=code",
                    $"&client_id={_clientData.ClientId}",
                    $"&state={shaHash}");
#if ANDROID
                _authUri += $"&redirect_uri={_clientData.RedirectUri}";
#endif
            }
            if (client == "Facebook")
            {
                _authUri = string.Concat("https://www.facebook.com/v20.0/dialog/oauth?",
                    $"client_id={_clientData.ClientId}",
                    $"&redirect_uri={_clientData.RedirectUri}",
                    $"&client_secret={_clientData.ClientSecret}",
                    $"&state={shaHash}");
            }

            return _authUri;
        }

        public async Task AuthenticateAsync(string client)
        {
            try
            {
                string accessToken = await StartTaskAsync(_authUri, _clientData.RedirectUri);

                _token = accessToken;
            }
            catch (TaskCanceledException)
            {
                return;
            }
        }

        public string GetToken()
            => _token;



        public async Task<string> StartTaskAsync(string oAuthUri, string callbackUri)
        {
#if WINDOWS
                var winUIAuthTools = new FixLife.ClientApp.Platforms.Windows.Tools.WinUIAuthTools(_clientData, (OAuthClientEnum)Enum.Parse(typeof(OAuthClientEnum), SelectedClient));
                winUIAuthTools.AuthRequest = oAuthUri;
                await winUIAuthTools.PrepareWebView();
                winUIAuthTools.StartLoopbackListener();
                oAuthUri += $"&redirect_uri={winUIAuthTools.LoopbackAddress}";
                var result = await winUIAuthTools.StartAndReturnOAuthProcess();
                return result;
#else
            var result = await WebAuthenticator.AuthenticateAsync(new Uri(oAuthUri), new Uri(callbackUri));
                return result?.AccessToken;
#endif
        }

        private async Task<OAuthClient> ReadDataFromJson(string client)
        {
            using var jsonStream = await FileSystem.OpenAppPackageFileAsync("oauthclients.json");
            using var jsonFile = new StreamReader(jsonStream);
            var jsonText = jsonFile.ReadToEnd();
            var clientsClass = JsonConvert.DeserializeObject<OAuthClientClass>(jsonText);
            var correctSystem = clientsClass.SystemProvider.Single(name => name.SystemName == DeviceInfo.Current.Platform.ToString());

            return client switch
            {
                "Google" => correctSystem.Google,
                "Facebook" => correctSystem.Facebook,
                _ => throw new Exception("Client not found")
            };

        }

        public void RunWebView(string uri)
        {
            _webViewBuilder = new WebViewBuilder(uri);
            _webViewBuilder.Show();
        }

        public void CloseWebView()
        {
            if (_webViewBuilder is null)
            {
                return;
            }

            _webViewBuilder.GoBack();
        }
    }
}
