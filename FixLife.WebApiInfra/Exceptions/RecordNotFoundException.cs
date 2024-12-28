namespace FixLife.WebApiInfra.Exceptions
{
    public class RecordNotFoundException : Exception
    {
        private const string Message = "Record not found on table.";
        
        public RecordNotFoundException() : base(Message) { 
            
        }

        public RecordNotFoundException(string message) : base(message)
        {

        }

        public RecordNotFoundException(string id, string entity) : base($"Record with id: {id}, was not found on: {entity} table.")
        {
        }
    }
}
