using FixLife.ClientApp.Common.Enums;
using FixLife.ClientApp.Common.Exceptions;
using FixLife.ClientApp.Common.WebAuthentication;
using FixLife.ClientApp.Common.WebAuthentication.Clients;
using FixLife.ClientApp.Common.WebAuthentication.Clients.AuthorizationResult;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace FixLife.ClientApp.Platforms.Windows.Tools
{
    public class WinUIAuthTools
    {
        public string LoopbackAddress { get; private set; }
        public OAuthClient _authClient { get; set; }
        public string AuthRequest { get; set; }

        private OAuthClientEnum _clientProvider;
        private HttpListener _listener;
        private HttpClient _httpClient;

        public WinUIAuthTools()
        {
            
        }

        public WinUIAuthTools(OAuthClient oAuthClient, OAuthClientEnum clientProvider)
        {
            _authClient = oAuthClient;
            _clientProvider = clientProvider;
        }

        public WinUIAuthTools(OAuthClientEnum clientProvider)
        {
            _clientProvider = clientProvider;
        }

        private string GetLoopbackWithPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            LoopbackAddress = $"http://{IPAddress.Loopback}:{port}/";
            return LoopbackAddress;
        }

        public void StartLoopbackListener()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add(GetLoopbackWithPort());
            _listener.Start();
        }

        public async Task PrepareWebView()
        {
            var webView = new WebViewBuilder(AuthRequest);
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            Grid.SetRow(webView.GetWebView(), 0);
            grid.Children.Add(webView.GetWebView());
            var page = new ContentPage
            {
                Content = grid
            };
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public async Task<string> StartAndReturnOAuthProcess()
        {
            var context = await _listener.GetContextAsync();
            var code = context.Request.QueryString["code"];
            var state = context.Request.QueryString["state"];
            if(IsCodeInvalidWithState(code, state))
            {
                throw new InvalidAuthorizationException(_clientProvider);
            }
            var token = ExchangeToToken(code, LoopbackAddress);

            return token;
        }

        private bool IsCodeInvalidWithState(string code, string state)
        {
            return string.IsNullOrEmpty(code) && code != state;
        }

        private string ExchangeToToken(string code, string redirectUri)
        {
            string tokenValue = "";
            _httpClient = new();

            switch(_clientProvider)
            {
                case OAuthClientEnum.Google:
                    var tokenUri = "https://www.googleapis.com/oauth2/v4/token";
                    var tokenRequestBody = string.Format("code={0}&client_id={1}&client_secret={2}&redirect_uri={3}&grant_type=authorization_code", code, _authClient.ClientId, _authClient.ClientSecret, redirectUri);
                    var request = new HttpRequestMessage(HttpMethod.Post, tokenUri)
                    {
                        Content = new StringContent(tokenRequestBody, Encoding.UTF8, "application/x-www-form-urlencoded"),
                    };

                    var response = _httpClient.SendAsync(request).Result;
                    if(response.IsSuccessStatusCode)
                    {
                        var authResult = JsonConvert.DeserializeObject<GoogleAuthorizationResult>(response.Content.ReadAsStringAsync().Result);
                        tokenValue = authResult.AccessToken;
                    }

                    break;
                case OAuthClientEnum.Facebook:
                    break;
                default:
                    throw new InvalidAuthorizationException(_clientProvider);
            }

            return tokenValue;
        }
    }
}
