namespace FixLife.WebApiInfra.Exceptions
{
    public class ConnStringFailException : Exception
    {
        private const string ExceptionString = "Cannot load Configuration Connection String section.";
        public ConnStringFailException() : base(ExceptionString)
        {          
        }
    }
}
