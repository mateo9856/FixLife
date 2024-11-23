using FixLife.WebApiInfra.Common;
using FixLife.WebApiInfra.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace FixLife.WebApiInfra.Contexts
{
    public class MongoContextFactory<TContext> : IMongoContextFactory<TContext> where TContext : DbContext
    {
        private readonly string _connectionString;
        private readonly MongoClient _client;
        private readonly DbContextOptionsBuilder<TContext> _optionsBuilder;

        public MongoContextFactory(IConfiguration configuration)
        {

            _connectionString = configuration.GetConnectionString("FixLife") 
                ?? throw new ConnStringFailException();

            _client = new MongoClient(_connectionString);

            _optionsBuilder = new DbContextOptionsBuilder<TContext>().UseMongoDB(_client, "FixLifeDb");

        }

        public TContext CreateDbInstance()
        {
            var dbCtx = Activator.CreateInstance(typeof(TContext), _optionsBuilder.Options)
                ?? throw new DbContextLoadException(typeof(TContext));

            return (TContext)dbCtx;
        }
    }
}
