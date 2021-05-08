using Couriers.BusinessService.Interfaces;
using Couriers.Database.Interfaces;
using Couriers.Database.Models;
using System.Collections.Generic;
using System.Linq;

namespace Couriers.BusinessService.Domain
{
    public class JobBusinessService : IJobBusinessService
    {
        private IJobRepository _jobRepository;
        private IJobStatusRepository _jobStatusRepository;

        public JobBusinessService(IJobRepository jobRepository, IJobStatusRepository jobStatusRepository)
        {
            _jobRepository = jobRepository;
            _jobStatusRepository = jobStatusRepository;
        }

        public List<Job> GetJobs(string search, string sortBy, int skip, int take)
        {
            var results = _jobRepository.GetJobs;

            if (!string.IsNullOrEmpty(search))
            {
                results = results.Where(t => t.Customer.Contains(search));
            }

            //sort the results
            switch (sortBy)
            {
                case "status":
                    results = results.OrderBy(j => j.JobStatus.Description);
                    break;
                case "status_desc":
                    results = results.OrderByDescending(j => j.JobStatus.Description);
                    break;
                case "goods":
                    results = results.OrderBy(j => j.Goods);
                    break;
                case "goods_desc":
                    results = results.OrderByDescending(t => t.Goods);
                    break;
                case "customer":
                    results = results.OrderBy(j => j.Customer);
                    break;
                case "customer_desc":
                default:
                    results = results.OrderByDescending(t => t.Customer);
                    break;
            }

            return results
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public int GetJobsCount(string search)
        {
            var results = _jobRepository.GetJobs;

            if (!string.IsNullOrEmpty(search))
            {
                results = results.Where(t => t.Customer.Contains(search));
            }

            return results.Count();
        }

        public Job GetJob(int jobId)
        {
            return _jobRepository.GetJob(jobId);
        }

        public int AddJob(Job job)
        {
            var jobId =_jobRepository.AddJob(job);

            return jobId;
        }

        public bool EditJob(Job job)
        {
            _jobRepository.EditJob(job);

            return true;
        }

        public bool DeleteJob(int jobId)
        {
            _jobRepository.DeleteJob(jobId);

            return true;
        }

        public List<JobStatus> GetStatuses()
        {
            return _jobStatusRepository.GetStatuses.ToList();
        }
    }
}
