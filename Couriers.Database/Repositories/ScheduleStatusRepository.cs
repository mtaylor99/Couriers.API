using Couriers.Database.Interfaces;
using Couriers.Database.Models;
using System.Linq;

namespace Couriers.Database.Repositories
{
    public class ScheduleStatusRepository : IScheduleStatusRepository
    {
        private CouriersDbContext context;

        public ScheduleStatusRepository(CouriersDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<ScheduleStatus> GetStatuses => context.ScheduleStatuses.OrderBy(j => j.ScheduleStatusId);
    }
}
