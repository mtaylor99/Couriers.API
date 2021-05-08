using AutoMapper;
using Couriers.BusinessService.Interfaces;
using Couriers.Database.Models;
using Couriers.ModelService.Interfaces;
using Couriers.ModelService.Models;
using System.Collections.Generic;

namespace Couriers.ModelService.Domain
{
    public class ScheduleModelService : IScheduleModelService
    {
        private readonly IMapper _mapper;
        private IScheduleBusinessService _scheduleBusinessService;

        public ScheduleModelService(IMapper mapper, IScheduleBusinessService scheduleBusinessService)
        {
            _mapper = mapper;
            _scheduleBusinessService = scheduleBusinessService;
        }

        public List<DtoSchedule> GetSchedules(string sortBy, int page, int pageSize)
        {
            var schedules = _scheduleBusinessService.GetSchedules(sortBy, page, pageSize);
            return _mapper.Map<List<DtoSchedule>>(schedules);
        }

        public int GetSchedulesCount()
        {
            return _scheduleBusinessService.GetSchedulesCount();
        }

        public DtoSchedule GetSchedule(int scheduleId)
        {
            var schedule = _scheduleBusinessService.GetSchedule(scheduleId);
            return _mapper.Map<DtoSchedule>(schedule);
        }

        public int AddSchedule(DtoSchedule dtoSchedule)
        {
            var schedule = _mapper.Map<Schedule>(dtoSchedule);
            return _scheduleBusinessService.AddSchedule(schedule);
        }

        public bool EditSchedule(DtoSchedule dtoSchedule)
        {
            var schedule = _mapper.Map<Schedule>(dtoSchedule);
            return _scheduleBusinessService.EditSchedule(schedule);
        }

        public bool DeleteSchedule(int scheduleId)
        {
            return _scheduleBusinessService.DeleteSchedule(scheduleId);
        }

        public List<DtoScheduleStatus> GetStatuses()
        {
            var statuses = _scheduleBusinessService.GetStatuses();
            return _mapper.Map<List<DtoScheduleStatus>>(statuses);
        }

        public List<DtoScheduleItem> GetScheduleItems(int scheduleId)
        {
            var scheduleItems = _scheduleBusinessService.GetScheduleItems(scheduleId);
            return _mapper.Map<List<DtoScheduleItem>>(scheduleItems);
        }

        public bool UpdateScheduleItems(int scheduleId, List<DtoScheduleItem> dtoScheduleItems)
        {
            var scheduleItems = _mapper.Map<List<ScheduleItem>>(dtoScheduleItems);
            return _scheduleBusinessService.UpdateScheduleItems(scheduleId, scheduleItems);
        }
    }
}
