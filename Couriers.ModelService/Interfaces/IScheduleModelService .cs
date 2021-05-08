using Couriers.ModelService.Models;
using System.Collections.Generic;

namespace Couriers.ModelService.Interfaces
{
    public interface IScheduleModelService
    {
        List<DtoSchedule> GetSchedules(string sortBy, int page, int pageSize);

        int GetSchedulesCount();

        DtoSchedule GetSchedule(int scheduleId);

        int AddSchedule(DtoSchedule dtoSchedule);

        bool EditSchedule(DtoSchedule dtoSchedule);

        bool DeleteSchedule(int scheduleId);

        List<DtoScheduleStatus> GetStatuses();

        List<DtoScheduleItem> GetScheduleItems(int scheduleId);

        bool UpdateScheduleItems(int scheduleId, List<DtoScheduleItem> scheduleItems);
    }
}
