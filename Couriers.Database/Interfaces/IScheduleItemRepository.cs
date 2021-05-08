using Couriers.Database.Models;
using System.Collections.Generic;
using System.Linq;

namespace Couriers.Database.Interfaces
{
    public interface IScheduleItemRepository
    {
        IQueryable<ScheduleItem> GetScheduleItems(int scheduleId);
        bool UpdateScheduleItems(int scheduleId, List<ScheduleItem> scheduleItems);
    }
}
