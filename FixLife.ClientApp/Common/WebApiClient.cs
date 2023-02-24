using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Common
{
    public static class WebApiClient
    {
        public const string ADDRESS = "http://localhost:18347";
        //TODO: API Service calls to GET,POST,PUT,DELETE Register
        //https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/rest?view=net-maui-7.0  read this
        public static async Task<string> CallServiceGetAsync(string address)
        {
            HttpClient client = new HttpClient();
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
    }
}
