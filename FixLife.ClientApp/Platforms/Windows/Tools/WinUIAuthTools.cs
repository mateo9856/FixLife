using System.Net;
using System.Net.Sockets;

namespace FixLife.ClientApp.Platforms.Windows.Tools
{
    public class WinUIAuthTools
    {
        private HttpListener _listener;
        public string LoopbackAddress { get; private set; }

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

        public async Task<HttpListenerResponse> StartAndReturnOAuthProcess(string authReqeust)
        {
            await Browser.Default.OpenAsync(authReqeust, BrowserLaunchMode.SystemPreferred);
            var context = await _listener.GetContextAsync();
            return context.Response;
        }

    }
}
