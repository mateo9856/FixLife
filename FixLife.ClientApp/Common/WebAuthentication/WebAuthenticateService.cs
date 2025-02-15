﻿using FixLife.ClientApp.Common.Abstraction;
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
                uri = string.Concat("https://accounts.google.com/o/oauth2/auth?",
                    "scope=email%20profile",
                    "&response_type=code",
                    $"&client_id={clientData.ClientId}",
                    $"&redirect_uri={clientData.RedirectUri}",
                    $"&state={stateParam}");
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
                string accessToken = await StartTaskAsync(uri, clientData.RedirectUri);

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
                var result = await WinUIEx.WebAuthenticator.AuthenticateAsync(new Uri(oAuthUri), new Uri(callbackUri), new CancellationTokenSource().Token);
                return result?.AccessToken;
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

    }
}
