using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Common
{
    public class AppHelper
    {
        private string GetAppSettingsPath()
        {
            var baseDir = AppContext.BaseDirectory;
            var subStrDir = baseDir.Substring(0, baseDir.IndexOf("ClientApp") + 9);
            return subStrDir; 
        }

        public async Task<bool> SetAppSettings(params (string, bool)[] values)
        {
            var path = GetAppSettingsPath();
            var jsonPath = await File.ReadAllTextAsync(Path.Combine(path, "appsettings.json"));
            dynamic jsonSettings = JsonConvert.DeserializeObject(jsonPath);
            foreach(var value in values)
            {
                jsonSettings["settings"][value.Item1] = value.Item2;
            }
            string output = JsonConvert.SerializeObject(jsonSettings, Formatting.Indented);
            await File.WriteAllTextAsync(Path.Combine(path, "appsettings.json"), output);
            return true;
        }
    }
}
