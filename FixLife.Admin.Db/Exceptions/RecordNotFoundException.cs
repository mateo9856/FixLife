namespace FixLife.Admin.Db.Exceptions
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException() : base("Record not found in database.") 
        { 
        }

        public RecordNotFoundException(string entityName) : base($"Record not found in {entityName} table.") 
        { 
        }
    }
}
