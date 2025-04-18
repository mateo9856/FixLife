namespace FixLife.ClientApp.Common.WebAuthentication.Clients
{
    public class OAuthClientClassObject
    {
        public OAuthClient Google { get; set; }
        public OAuthClient Facebook { get; set; }
        public string SystemName { get; set; }
    }

    public class OAuthClientClass
    {
        public List<OAuthClientClassObject> SystemProvider { get; set; }
    }
}
