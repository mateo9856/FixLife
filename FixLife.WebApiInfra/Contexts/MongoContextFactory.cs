using FixLife.WebApiInfra.Common;
using FixLife.WebApiInfra.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace FixLife.WebApiInfra.Contexts
{
    public class MongoContextFactory<T> : IMongoContextFactory<T> where T : DbContext
    {
        private readonly string _connectionString;
        private readonly MongoClient _client;
        private readonly DbContextOptionsBuilder<T> _optionsBuilder;

        public MongoContextFactory(IConfiguration configuration)
        {

            _connectionString = configuration.GetConnectionString("FixLife") 
                ?? throw new ConnStringFailException();

            _client = new MongoClient(_connectionString);

            _optionsBuilder = new DbContextOptionsBuilder<T>().UseMongoDB(_client, "FixLifeDb");

        }

        public T CreateDbInstance()
        {
            return (T)new DbContext(_optionsBuilder.Options);
        }
    }
}
