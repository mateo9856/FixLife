using FixLife.WebApiInfra.Common;
using FixLife.WebApiInfra.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace FixLife.WebApiInfra.Contexts
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly string _connectionString;
        
        public DbContextFactory(IConfiguration configuration)
        {

            _connectionString = configuration.GetConnectionString("FixLife") 
                ?? throw new ConnStringFailException();

            ConfigContexts();
            
        }

        private void ConfigContexts()
        {
            var mongoClient = new MongoClient(_connectionString);

            var AppDbOptions = new DbContextOptionsBuilder<ApplicationContext>().UseMongoDB(mongoClient, "FixLifeDb");
            var IdDbOptions = new DbContextOptionsBuilder<IdentityContext>().UseMongoDB(mongoClient, "FixLifeDb");
        
            AppContext = new ApplicationContext(AppDbOptions.Options);
            IdContext = new IdentityContext(IdDbOptions.Options);
        }

        public ApplicationContext AppContext { get; private set; }

        public IdentityContext IdContext { get; private set; }
    }
}
