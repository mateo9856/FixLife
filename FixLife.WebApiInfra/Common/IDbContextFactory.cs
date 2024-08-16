using FixLife.WebApiInfra.Contexts;

namespace FixLife.WebApiInfra.Common
{
    public interface IDbContextFactory
    {
        public ApplicationContext AppContext { get; }
        public IdentityContext IdContext { get; }
    }
}
