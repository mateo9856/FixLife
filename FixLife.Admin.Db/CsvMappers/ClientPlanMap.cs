using CsvHelper.Configuration;
using FixLife.Admin.Db.Entities;

namespace FixLife.Admin.Db.CsvMappers
{
    public class ClientPlanMap : ClassMap<ClientPlan>
    {
        public ClientPlanMap()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.WeeklyWork.TimeStart).Name("WeeklyWorkStart");
            Map(m => m.WeeklyWork.TimeEnd).Name("WeeklyWorkEnd");
            Map(m => m.LearnTime.StartTime).Name("LearnTimeStart");
            Map(m => m.LearnTime.TimeInterval).Name("LearnTimeInterval");
            Map(m => m.FreeTime.TimeStart).Name("FreeTimeStart");
            Map(m => m.FreeTime.TimeEnd).Name("FreeTimeEnd");
        }
    }
}
