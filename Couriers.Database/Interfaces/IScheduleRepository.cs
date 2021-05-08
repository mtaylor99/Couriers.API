using Couriers.Database.Models;
using System.Linq;

namespace Couriers.Database.Interfaces
{
    public interface IScheduleRepository
    {
        IQueryable<Schedule> GetSchedules { get; }
        Schedule GetSchedule(int scheduleId);
        int AddSchedule(Schedule schedule);
        void EditSchedule(Schedule schedule);
        Schedule DeleteSchedule(int scheduleId);
    }
}
