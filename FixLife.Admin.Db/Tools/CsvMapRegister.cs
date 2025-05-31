using CsvHelper;
using FixLife.Admin.Db.CsvMappers;
using FixLife.Admin.Db.Entities;
using System.Runtime.CompilerServices;

namespace FixLife.Admin.Db.Tools
{
    public static class CsvMapRegister
    {
        public static void RegisterAllMaps(this CsvWriter csv)
        {
            if (csv == null)
            {
                throw new ArgumentNullException(nameof(csv), "CsvWriter cannot be null.");
            }

            csv.Context.RegisterClassMap<ClientPlanMap>();
        }

        public static void RegisterByEntity<T>(this CsvWriter csv) where T : class
        {
            if (csv == null)
            {
                throw new ArgumentNullException(nameof(csv), "CsvWriter cannot be null.");
            }
            if (typeof(T) == typeof(ClientPlan))
            {
                csv.Context.RegisterClassMap<ClientPlanMap>();
            }
            else
            {
                throw new NotSupportedException($"No CSV map registered for type {typeof(T).Name}");
            }
        }
    }
}
