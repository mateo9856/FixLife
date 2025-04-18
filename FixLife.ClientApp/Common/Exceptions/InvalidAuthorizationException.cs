using FixLife.ClientApp.Common.Enums;

namespace FixLife.ClientApp.Common.Exceptions
{
    public class InvalidAuthorizationException : Exception
    {
        public InvalidAuthorizationException(OAuthClientEnum client) : base($"Invalid authorization for {client} OAuth client.")
        {

        }
        public InvalidAuthorizationException() : base("Invalid authorization.")
        {
            
        }
    }
}
