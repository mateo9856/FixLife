using Newtonsoft.Json;

namespace FixLife.ClientApp.Options
{
    public static class SettingsOperations
    {
        private static string _path { get; set; }
        public static void LoadOrCreateSettings()
        {
            try
            {
                _path = Path.Combine(FileSystem.Current.AppDataDirectory, "clientSettings.json");
                var data = File.ReadAllText(_path);

            } catch(Exception)
            {
                CreateSettings();
            }
        }
        private static void CreateSettings()
        {
            var optionsObj = new ClientOptions
            {
                LightTheme = false,
                NotificationEnabled = false,
                OldPlansToFileEnabled = false,
                ShareEnabled = false
            };
            var jsonFormat = JsonConvert.SerializeObject(optionsObj);
            File.WriteAllText(_path, jsonFormat);
        }

        public static ClientOptions LoadToOptionsPage()
        {
            var jsonToDeserialize = File.ReadAllText(_path);
            return JsonConvert.DeserializeObject<ClientOptions>(jsonToDeserialize);
        }

        public static bool SaveNewSettings(ClientOptions options)
        {
            try
            {
                var serializeOptions = JsonConvert.SerializeObject(options);
                File.WriteAllText(_path, serializeOptions);
                return true;

            } catch (Exception)
            {
                return false;
            }
        }
    }
}
