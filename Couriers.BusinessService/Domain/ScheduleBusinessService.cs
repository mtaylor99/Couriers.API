using Couriers.BusinessService.Interfaces;
using Couriers.Database.Interfaces;
using Couriers.Database.Models;
using System.Collections.Generic;
using System.Linq;

namespace Couriers.BusinessService.Domain
{
    public class ScheduleBusinessService : IScheduleBusinessService
    {
        private IScheduleRepository _scheduleRepository;
        private IScheduleItemRepository _scheduleItemRepository;
        private IScheduleStatusRepository _scheduleStatusRepository;

        public ScheduleBusinessService(IScheduleRepository scheduleRepository, IScheduleItemRepository scheduleItemRepository, IScheduleStatusRepository scheduleStatusRepository)
        {
            _scheduleRepository = scheduleRepository;
            _scheduleItemRepository = scheduleItemRepository;
            _scheduleStatusRepository = scheduleStatusRepository;
        }

        public List<Schedule> GetSchedules(string sortBy, int skip, int take)
        {
            var results = _scheduleRepository.GetSchedules;

            //sort the results
            switch (sortBy)
            {
                case "scheduleId":
                    results = results.OrderBy(s => s.ScheduleId);
                    break;
                case "scheduleId_desc":
                    results = results.OrderByDescending(s => s.ScheduleId);
                    break;
                case "status":
                    results = results.OrderBy(s => s.ScheduleStatus.Description);
                    break;
                case "status_desc":
                    results = results.OrderByDescending(s => s.ScheduleStatus.Description);
                    break;
                default:
                    results = results.OrderByDescending(s => s.ScheduleId);
                    break;
            }

            return results
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public int GetSchedulesCount()
        {
            var results = _scheduleRepository.GetSchedules;

           return results.Count();
        }

        public Schedule GetSchedule(int scheduleId)
        {
            return _scheduleRepository.GetSchedule(scheduleId);
        }

        public int AddSchedule(Schedule schedule)
        {
            var jobId = _scheduleRepository.AddSchedule(schedule);

            return jobId;
        }

        public bool EditSchedule(Schedule schedule)
        {
            _scheduleRepository.EditSchedule(schedule);

            return true;
        }

        public bool DeleteSchedule(int scheduleId)
        {
            _scheduleRepository.DeleteSchedule(scheduleId);

            return true;
        }

        public List<ScheduleStatus> GetStatuses()
        {
            return _scheduleStatusRepository.GetStatuses.ToList();
        }

        public List<ScheduleItem> GetScheduleItems(int scheduleId)
        {
            return _scheduleItemRepository.GetScheduleItems(scheduleId).ToList();
        }

        public bool UpdateScheduleItems(int scheduleId, List<ScheduleItem> scheduleItems)
        {
            _scheduleItemRepository.UpdateScheduleItems(scheduleId, scheduleItems);

            return true;
        }
    }
}
