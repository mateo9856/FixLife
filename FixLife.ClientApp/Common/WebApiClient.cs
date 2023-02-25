using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ThreadNetwork;

namespace FixLife.ClientApp.Common
{
    public class WebApiClient : IDisposable
    {
        public const string ADDRESS = "http://localhost:18347";

        HttpClient client;
        JsonSerializerOptions _serializerOptions;
        public WebApiClient()
        {
            client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<string> CallServiceGetAsync(string address)
        {
            Uri uri = new Uri(string.Format("{0}/api/{1}", ADDRESS, address));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                return await response.Content.ReadAsStringAsync();

            } catch(Exception ex)
            {
                throw;
            }
            
        }

        public async Task<string> PostPutAsync(object element, string address, bool isPost)
        {
            Uri uri = new Uri(string.Format("{0}/api/{1}", ADDRESS, address));
            try
            {
                string json = JsonSerializer.Serialize(element, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                if (isPost)
                    response = await client.PostAsync(uri, content);
                else
                    response = await client.PutAsync(uri, content);

                return await response.Content.ReadAsStringAsync();

            } catch(Exception ex)
            {
                throw;
            }


        }

        public async Task<string> DeleteAsync(string address)
        {
            Uri uri = new Uri(string.Format("{0}/api/{1}", ADDRESS, address));
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(uri);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Dispose()
        {
            client.Dispose();
            _serializerOptions = null;
        }
    }
}
