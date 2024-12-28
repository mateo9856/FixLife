namespace FixLife.WebApiInfra.Exceptions
{
    public class DbContextLoadException : Exception
    {
        public override string Message { get; }
        
        public DbContextLoadException(Type obj)
        {
            Message = $"Cannot load DbContext with Type: {obj.Name}";
        }
    }
}
