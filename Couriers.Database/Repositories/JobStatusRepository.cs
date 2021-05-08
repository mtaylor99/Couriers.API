using Couriers.Database.Interfaces;
using Couriers.Database.Models;
using System.Linq;

namespace Couriers.Database.Repositories
{
    public class JobStatusRepository : IJobStatusRepository
    {
        private CouriersDbContext context;

        public JobStatusRepository(CouriersDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<JobStatus> GetStatuses => context.JobStatuses.OrderBy(j => j.JobStatusId);
    }
}
