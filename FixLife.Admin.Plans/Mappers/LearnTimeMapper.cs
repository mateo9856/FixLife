using FixLife.Admin.Db.Tools.Abstraction;

namespace FixLife.Admin.Plans.Mappers
{
    public class LearnTimeMapper : IModelMapper<FixLife.Admin.Plans.Models.LearnTime, FixLife.Admin.Db.Entities.Plans.LearnTime>
    {
        public Db.Entities.Plans.LearnTime Map(Models.LearnTime source)
        {
            return new Db.Entities.Plans.LearnTime
            {
                Id = source.Id,
                TimeInterval = source.TimeInterval,
                StartTime = source.StartTime,
                DayOfWeeks = source.DayOfWeeks.Select(d => Enum.Parse<FixLife.Admin.Db.Enums.DayOfWeeks>(d.ToString())).ToList(),
                CreatedAt = DateTime.UtcNow,
            };
        }

        public Models.LearnTime MapBack(Db.Entities.Plans.LearnTime source)
        {
            return new Models.LearnTime
            {
                Id = source.Id,
                TimeInterval = source.TimeInterval,
                StartTime = source.StartTime,
                DayOfWeeks = source.DayOfWeeks.Select(d => Enum.Parse<FixLife.Admin.Plans.Enums.DayOfWeeks>(d.ToString())).ToList(),
            };
        }
    }
}
