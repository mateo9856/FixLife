﻿using Newtonsoft.Json;
using System.Text;


namespace FixLife.ClientApp.Common
{
    public class WebApiClient<T> : IDisposable
    {
        private readonly string ADDRESS = GetAddressByPlatform();

        HttpClient client;
        public WebApiClient()
        {
            client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<T> CallServiceGetAsync(string address, object element = null, string token = null)
        {
            Uri uri = new Uri(string.Format("{0}/api/{1}", ADDRESS, address));
            try
            {

                var message = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = uri,
                    Content = element != null ? new StringContent(JsonConvert.SerializeObject(element), Encoding.UTF8) : null
                };

                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }

                var response = await client.GetAsync(uri);
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);

            } catch(Exception ex)
            {
                var msg = ex.Message;
                throw;
            }
            
        }

        public async Task<T> PostPutAsync(object element, string address, bool isPost, string token = null)
        {
            Uri uri = new Uri(string.Format("{0}/api/{1}", ADDRESS, address));
            try
            {
                HttpMethod method = isPost ? HttpMethod.Post : HttpMethod.Put;
                HttpRequestMessage request = new HttpRequestMessage(method, uri);
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                string json = JsonConvert.SerializeObject(element);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = content;
                HttpResponseMessage response = await client.SendAsync(request);

                //response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(result);

            }
            catch (Exception)
            {
                throw;
            }


        }

        public async Task<string> DeleteAsync(string address, string token = null)
        {
            Uri uri = new Uri(string.Format("{0}/api/{1}", ADDRESS, address));
            try
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, uri);
                if (!string.IsNullOrEmpty(token))
                {
                    message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                var response = await client.DeleteAsync(uri);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            client.Dispose();
        }

        private static string GetAddressByPlatform()
        {

            var currentPlatform = DeviceInfo.Current.Platform;

            if (currentPlatform == DevicePlatform.Android)
            {
                return "http://10.0.2.2:5141";
            }
            else
                return "https://localhost:7021";
        }
    }
}
