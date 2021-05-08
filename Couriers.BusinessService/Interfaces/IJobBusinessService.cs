using Couriers.Database.Models;
using System.Collections.Generic;

namespace Couriers.BusinessService.Interfaces
{
    public interface IJobBusinessService
    {
        List<Job> GetJobs(string search, string sortBy, int skip, int take);

        int GetJobsCount(string search);

        Job GetJob(int jobId);

        int AddJob(Job job);

        bool EditJob(Job job);

        bool DeleteJob(int jobId);

        List<JobStatus> GetStatuses();
    }
}
