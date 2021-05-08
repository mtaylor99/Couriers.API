using Couriers.Database.Models;
using System.Collections.Generic;

namespace Couriers.BusinessService.Interfaces
{
    public interface IScheduleBusinessService
    {
        List<Schedule> GetSchedules(string sortBy, int skip, int take);

        int GetSchedulesCount();

        Schedule GetSchedule(int scheduleId);

        int AddSchedule(Schedule schedule);

        bool EditSchedule(Schedule schedule);

        bool DeleteSchedule(int scheduleId);

        List<ScheduleStatus> GetStatuses();

        List<ScheduleItem> GetScheduleItems(int scheduleId);

        bool UpdateScheduleItems(int scheduleId, List<ScheduleItem> scheduleItems);
    }
}
