using Couriers.Database.Interfaces;
using Couriers.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Couriers.Database.Repositories
{
    public class JobRepository : IJobRepository
    {
        private CouriersDbContext context;

        public JobRepository(CouriersDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Job> GetJobs => context.Jobs
            .Include(l => l.JobLines)
            .ThenInclude(c => c.CollectionAddress)
            .Include(l => l.JobLines)
            .ThenInclude(d => d.DeliveryAddress)
            .OrderByDescending(j => j.CreatedDate);

        public Job GetJob(int jobId)
        {
            return context.Jobs.FirstOrDefault(j => j.JobId == jobId);
        }

        public int AddJob(Job job)
        {
            try
            {
                context.Jobs.Add(job);
                context.SaveChanges();

                return job.JobId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public void EditJob(Job job)
        {
            if (job.JobId > 0)
            {
                Job dbEntry = context.Jobs.FirstOrDefault(j => j.JobId == job.JobId);

                if (dbEntry != null)
                {
                    dbEntry.Customer = job.Customer;
                    dbEntry.Goods = job.Goods;
                    dbEntry.JobStatusId = job.JobStatusId;
                    context.SaveChanges();
                }
            }
        }

        public Job DeleteJob(int jobId)
        {
            Job dbEntry = context.Jobs.FirstOrDefault(j => j.JobId == jobId);

            if (dbEntry != null)
            {
                context.Jobs.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }
    }
}
