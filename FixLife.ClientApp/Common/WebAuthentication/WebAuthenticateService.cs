using FixLife.ClientApp.Common.Abstraction;
using FixLife.ClientApp.Common.Enums;
using FixLife.ClientApp.Common.WebAuthentication.Clients;
using FixLife.ClientApp.Common.WebAuthentication.Clients.AuthorizationResult;
using FixLife.ClientApp.Sessions;
using Newtonsoft.Json;
using System.Text;

namespace FixLife.ClientApp.Common.WebAuthentication
{
    public class WebAuthenticateService(WebApiClient<object> webApiClient) : IWebAuthenticateService
    {
        private string _token = string.Empty;
        private string _authUri = string.Empty;

        private readonly WebApiClient<object> _webApiClient = webApiClient;

        private WebViewBuilder _webViewBuilder;
        private OAuthClient _clientData;
        public string SelectedClient { get; set; }

        public string LoadOAuthUri(string client)
        {
            SelectedClient = client;

            var stateParam = Guid.NewGuid().ToString();
            var shaHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(stateParam));
            _clientData = ReadDataFromJson(client);

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

                var userData = await GetUserData(_token);

                AddTokenToSession(_token, userData.Email);

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
                winUIAuthTools.StartLoopbackListener();
                
                oAuthUri += $"&redirect_uri={winUIAuthTools.LoopbackAddress}";
                
                var webView = GetWebView(oAuthUri);
                Application.Current.MainPage.Navigation.PushAsync(webView);
                
                var result = await winUIAuthTools.StartAndReturnOAuthProcess();

                Application.Current.MainPage.Navigation.PopAsync(true);
                
                return result;
#else
            var result = await WebAuthenticator.AuthenticateAsync(new Uri(oAuthUri), new Uri(callbackUri));
            return result?.AccessToken;
#endif
        }

        private OAuthClient ReadDataFromJson(string client)
        {
            using var jsonStream = FileSystem.OpenAppPackageFileAsync("oauthclients.json").Result;
            using var jsonFile = new StreamReader(jsonStream);
            var jsonText = jsonFile.ReadToEnd();
            var clientsClass = JsonConvert.DeserializeObject<OAuthClientClass>(jsonText);
            var correctSystem = clientsClass.SystemProvider
                .Single(name => name.SystemName == DeviceInfo.Current.Platform.ToString());

            return client switch
            {
                "Google" => correctSystem.Google,
                "Facebook" => correctSystem.Facebook,
                _ => throw new Exception("Client not found")
            };

        }

        private ContentPage GetWebView(string uri)
        {
            _webViewBuilder = new WebViewBuilder(uri);
            return _webViewBuilder.GetWebView();
        }

        public void CloseWebView()
        {
            if (_webViewBuilder is null)
            {
                return;
            }

            _webViewBuilder.GoBack();
        }

        public void AddTokenToSession(string token, string email)
        {
            UserSession.Token = token;
            UserSession.Email = email;
        }

        public async Task LogonByOAuthToken()
        {
            var addCommand = new
            {
                Token = _token,
                Email = UserSession.Email
            };

            await _webApiClient.PostPutAsync(new { Token = _token }, "Account/LoginByOAuth", true);
            
        }

        private async Task<OAuthUserData> GetUserData(string token)
        {
            var uri = $"https://www.googleapis.com/oauth2/v1/userinfo?access_token={token}";

            using var client = new HttpClient();
            var response = await client.GetAsync(uri)
                .Result
                .Content
                .ReadAsStringAsync();

            return JsonConvert.DeserializeObject<OAuthUserData>(response);
        }

    }
}
