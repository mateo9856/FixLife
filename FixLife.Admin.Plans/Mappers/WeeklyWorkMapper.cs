using FixLife.Admin.Db.Tools.Abstraction;

namespace FixLife.Admin.Plans.Mappers
{
    public class WeeklyWorkMapper : IModelMapper<FixLife.Admin.Plans.Models.WeeklyWork, FixLife.Admin.Db.Entities.Plans.WeeklyWork>
    {
        public Db.Entities.Plans.WeeklyWork Map(Models.WeeklyWork source)
        {
            return new Db.Entities.Plans.WeeklyWork
            {
                Id = source.Id,
                TimeStart = source.TimeStart,
                TimeEnd = source.TimeEnd,
                DayOfWeeks = source.DayOfWeeks.Select(d => Enum.Parse<FixLife.Admin.Db.Enums.DayOfWeeks>(d.ToString())).ToList(),
                CreatedAt = DateTime.UtcNow,
            };
        }

        public Models.WeeklyWork MapBack(Db.Entities.Plans.WeeklyWork source)
        {
            return new Models.WeeklyWork
            {
                Id = source.Id,
                TimeStart = source.TimeStart,
                TimeEnd = source.TimeEnd,
                DayOfWeeks = source.DayOfWeeks.Select(d => Enum.Parse<FixLife.Admin.Plans.Enums.DayOfWeeks>(d.ToString())).ToList(),
            };
        }
    }
}
