using FixLife.ClientApp.Common.Enums;
using FixLife.ClientApp.Common.Exceptions;
using System.Net;
using System.Net.Sockets;

namespace FixLife.ClientApp.Platforms.Windows.Tools
{
    public class WinUIAuthTools
    {
        private HttpListener _listener;
        public string LoopbackAddress { get; private set; }
        private OAuthClientEnum _clientProvider;

        public WinUIAuthTools()
        {
            
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

        public async Task<string> StartAndReturnOAuthProcess(string authReqeust)
        {
            await Browser.Default.OpenAsync(authReqeust, BrowserLaunchMode.SystemPreferred);
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
            //TODO: Implement token exchange
            return "";
        }
    }
}
