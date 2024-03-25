namespace FixLife.ClientApp.Options
{
    public class ApiConnectionOptions
    {
        public const string ApiConnection = "ApiConnection";

        public bool HttpsConnection { get => Convert.ToBoolean(TrustConnection); }

        public string TrustConnection { get; set; }
        public string Windows { get; set; }
        public string Android { get; set; }
        public string AndroidHttps { get; set; }
    }
}
