using CsvHelper;
using System.Globalization;

namespace FixLife.Admin.Db.Tools
{
    public class CsvService<T> where T : class
    {
        public void SaveToCsv(string fileName, T record, string dir = null)
        { 
            if (string.IsNullOrEmpty(dir))
            {
                dir = Directory.GetCurrentDirectory();
            }

            if (record == null || string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("Invalid arguments provided.");
            }

            using var writer = new StreamWriter(Path.Combine(dir, fileName), append: true);
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.RegisterByEntity<T>();
            csvWriter.WriteRecord<T>(record);
        }

        public void SaveMultipleRecords(string fileName, IEnumerable<T> records, string dir = null)
        {
            if (string.IsNullOrEmpty(dir))
            {
                dir = Directory.GetCurrentDirectory();
            }
            if (records == null || !records.Any() || string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("Invalid arguments provided.");
            }
            using var writer = new StreamWriter(Path.Combine(dir, fileName), append: true);
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.RegisterByEntity<T>();
            csvWriter.WriteRecords(records);
        }
    }
}
