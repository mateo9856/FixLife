using System.Data.Common;

namespace FixLife.Admin.Db.Exceptions
{
    public class ConnectionStringException : DbException
    {
        public ConnectionStringException() : base("Error through connect to Database, please check connectionString config.") 
        {
        }

        public ConnectionStringException(string dbName) : base($"Error through connect to {dbName} Database, please check connectionString config.")
        { 
        }
    }
}
