namespace FixLife.WebApiDomain.Exceptions
{
    public class PlanNotFoundException : Exception
    {
        public PlanNotFoundException(string message) : base(message) 
        { }
    }
}
