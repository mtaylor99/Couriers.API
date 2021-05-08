using Couriers.ModelService.Models;
using System.Collections.Generic;

namespace Couriers.ModelService.Interfaces
{
    public interface IJobModelService
    {
        List<DtoJob> GetJobs(string search, string sortBy, int page, int pageSize);

        int GetJobsCount(string search);

        DtoJob GetJob(int jobId);

        int AddJob(DtoJob dtoJob);

        bool EditJob(DtoJob dtoJob);

        bool DeleteJob(int jobId);

        List<DtoJobStatus> GetStatuses();
    }
}
