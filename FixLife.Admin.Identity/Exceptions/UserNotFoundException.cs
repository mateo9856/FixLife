namespace FixLife.Admin.Identity.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User not found.")
        { 
        }

        public UserNotFoundException(string userName) : base($"User: {userName} is not found.")
        {
        }
    }
}
