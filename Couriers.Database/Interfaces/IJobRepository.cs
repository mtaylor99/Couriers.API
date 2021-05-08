using Couriers.Database.Models;
using System.Linq;

namespace Couriers.Database.Interfaces
{
    public interface IJobRepository
    {
        IQueryable<Job> GetJobs { get; }
        Job GetJob(int jobId);
        int AddJob(Job job);
        void EditJob(Job job);
        Job DeleteJob(int jobId);
    }
}
