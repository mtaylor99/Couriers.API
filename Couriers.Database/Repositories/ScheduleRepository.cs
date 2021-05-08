using Couriers.Database.Interfaces;
using Couriers.Database.Models;
using System;
using System.Linq;

namespace Couriers.Database.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private CouriersDbContext context;

        public ScheduleRepository(CouriersDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Schedule> GetSchedules => context.Schedules
            .OrderByDescending(s => s.ScheduleId);

        public Schedule GetSchedule(int scheduleId)
        {
            return context.Schedules.FirstOrDefault(s => s.ScheduleId == scheduleId);
        }

        public int AddSchedule(Schedule schedule)
        {
            try
            {
                context.Schedules.Add(schedule);
                context.SaveChanges();

                return schedule.ScheduleId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public void EditSchedule(Schedule schedule)
        {
            if (schedule.ScheduleId > 0)
            {
                Schedule dbEntry = context.Schedules.FirstOrDefault(s => s.ScheduleId == schedule.ScheduleId);

                if (dbEntry != null)
                {
                    dbEntry.DriverId = schedule.DriverId;
                    dbEntry.Date = schedule.Date;
                    dbEntry.ScheduleStatusId = schedule.ScheduleStatusId;
                    context.SaveChanges();
                }
            }
        }

        public Schedule DeleteSchedule(int scheduleId)
        {
            Schedule dbEntry = context.Schedules.FirstOrDefault(s => s.ScheduleId == scheduleId);

            if (dbEntry != null)
            {
                context.Schedules.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }
    }
}
