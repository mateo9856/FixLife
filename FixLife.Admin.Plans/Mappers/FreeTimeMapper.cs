using FixLife.Admin.Db.Tools.Abstraction;

namespace FixLife.Admin.Plans.Mappers
{
    public class FreeTimeMapper : IModelMapper<FixLife.Admin.Plans.Models.FreeTime, FixLife.Admin.Db.Entities.Plans.FreeTime>
    {
        public Db.Entities.Plans.FreeTime Map(Models.FreeTime source)
        {
            return new Db.Entities.Plans.FreeTime
            {
                Id = source.Id,
                TimeStart = source.TimeStart,
                TimeEnd = source.TimeEnd,
                Text = source.Text,
            };
        }
        public Models.FreeTime MapBack(Db.Entities.Plans.FreeTime source)
        {
            return new Models.FreeTime
            {
                Id = source.Id,
                TimeStart = source.TimeStart,
                TimeEnd = source.TimeEnd,
                Text = source.Text,
            };
        }
    }
}
