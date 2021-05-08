using AutoMapper;
using Couriers.BusinessService.Interfaces;
using Couriers.Database.Models;
using Couriers.ModelService.Interfaces;
using Couriers.ModelService.Models;
using System.Collections.Generic;

namespace Couriers.ModelService.Domain
{
    public class JobModelService : IJobModelService
    {
        private readonly IMapper _mapper;
        private IJobBusinessService _jobBusinessService;

        public JobModelService(IMapper mapper, IJobBusinessService jobBusinessService)
        {
            _mapper = mapper;
            _jobBusinessService = jobBusinessService;
        }

        public List<DtoJob> GetJobs(string search, string sortBy, int page, int pageSize)
        {
            var jobs = _jobBusinessService.GetJobs(search, sortBy, page, pageSize);
            return _mapper.Map<List<DtoJob>>(jobs);
        }

        public int GetJobsCount(string search)
        {
            return _jobBusinessService.GetJobsCount(search);
        }

        public DtoJob GetJob(int jobId)
        {
            var job = _jobBusinessService.GetJob(jobId);
            return _mapper.Map<DtoJob>(job);
        }

        public int AddJob(DtoJob dtoJob)
        {
            var job = _mapper.Map<Job>(dtoJob);
            return _jobBusinessService.AddJob(job);
        }

        public bool EditJob(DtoJob dtoJob)
        {
            var job = _mapper.Map<Job>(dtoJob);
            return _jobBusinessService.EditJob(job);
        }

        public bool DeleteJob(int jobId)
        {
            return _jobBusinessService.DeleteJob(jobId);
        }

        public List<DtoJobStatus> GetStatuses()
        {
            var statuses = _jobBusinessService.GetStatuses();
            return _mapper.Map<List<DtoJobStatus>>(statuses);
        }
    }
}
