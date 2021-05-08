using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Couriers.ModelService.Interfaces;
using Couriers.ModelService.Models;
using Microsoft.AspNetCore.Http;

namespace Couriers.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private IJobModelService _jobModelService;

        public JobController(IJobModelService jobModelService)
        {
            _jobModelService = jobModelService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string search, [FromQuery] string sortBy, [FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = _jobModelService.GetJobs(search, sortBy, skip, take);
            var count = _jobModelService.GetJobsCount(search);

            var response = new DtoGridResponse<DtoJob> { Result = result, Count = count };
            var json = JsonConvert.SerializeObject(response);

            return Ok(json);
        }

        [HttpGet]
        [Route("count")]
        public IActionResult GetCount([FromQuery] string search)
        {
            var data = _jobModelService.GetJobsCount(search);

            var json = JsonConvert.SerializeObject(data);

            return Ok(json);
        }

        [HttpGet("{id}")]
        public IActionResult GetJob([FromRoute] int id)
        {
            var data = _jobModelService.GetJob(id);

            var json = JsonConvert.SerializeObject(data);

            return Ok(json);
        }

        [HttpPost]
        public IActionResult Post( [FromBody] DtoJob job)
        {
            var data = _jobModelService.AddJob(job);

            var json = JsonConvert.SerializeObject(data);

            return new ObjectResult(json) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] DtoJob job)
        {
            var data = _jobModelService.EditJob(job);

            var json = JsonConvert.SerializeObject(data);

            return new ObjectResult(json) { StatusCode = StatusCodes.Status204NoContent };
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var data = _jobModelService.DeleteJob(id);

            var json = JsonConvert.SerializeObject(data);

            return new ObjectResult(id) { StatusCode = StatusCodes.Status204NoContent };
        }

        [HttpGet]
        [Route("statuses")]
        public IActionResult GetStatuses()
        {
            var data = _jobModelService.GetStatuses();

            var json = JsonConvert.SerializeObject(data);

            return Ok(json);
        }
    }
}