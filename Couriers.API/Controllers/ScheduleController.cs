using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Couriers.ModelService.Interfaces;
using Couriers.ModelService.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Couriers.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : ControllerBase
    {
        private IScheduleModelService _scheduleModelService;

        public ScheduleController(IScheduleModelService scheduleModelService)
        {
            _scheduleModelService = scheduleModelService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string sortBy, [FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var result = _scheduleModelService.GetSchedules(sortBy, skip, take);
            var count = _scheduleModelService.GetSchedulesCount();

            var response = new DtoGridResponse<DtoSchedule> { Result = result, Count = count };
            var json = JsonConvert.SerializeObject(response);

            return Ok(json);
        }

        [HttpGet]
        [Route("count")]
        public IActionResult GetCount()
        {
            var data = _scheduleModelService.GetSchedulesCount();

            var json = JsonConvert.SerializeObject(data);

            return Ok(json);
        }

        [HttpGet("{id}")]
        public IActionResult GetSchedule([FromRoute] int id)
        {
            var data = _scheduleModelService.GetSchedule(id);

            var json = JsonConvert.SerializeObject(data);

            return Ok(json);
        }

        [HttpPost]
        public IActionResult Post( [FromBody] DtoSchedule schedule)
        {
            var data = _scheduleModelService.AddSchedule(schedule);

            var json = JsonConvert.SerializeObject(data);

            return new ObjectResult(json) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] DtoSchedule schedule)
        {
            var data = _scheduleModelService.EditSchedule(schedule);

            var json = JsonConvert.SerializeObject(data);

            return new ObjectResult(json) { StatusCode = StatusCodes.Status204NoContent };
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var data = _scheduleModelService.DeleteSchedule(id);

            var json = JsonConvert.SerializeObject(data);

            return new ObjectResult(id) { StatusCode = StatusCodes.Status204NoContent };
        }

        [HttpGet]
        [Route("scheduleItems")]
        public IActionResult GetScheduleItems([FromRoute] int scheduleId)
        {
            var data = _scheduleModelService.GetScheduleItems(scheduleId);

            var json = JsonConvert.SerializeObject(data);

            return Ok(json);
        }

        [HttpPut("{id}")]
        [Route("updateScheduleItems")]
        public IActionResult UpdateScheduleItems([FromRoute] int id, [FromBody] List<DtoScheduleItem> scheduleItems)
        {
            var data = _scheduleModelService.UpdateScheduleItems(id, scheduleItems);

            var json = JsonConvert.SerializeObject(data);

            return new ObjectResult(json) { StatusCode = StatusCodes.Status204NoContent };
        }

        [HttpGet]
        [Route("statuses")]
        public IActionResult GetStatuses()
        {
            var data = _scheduleModelService.GetStatuses();

            var json = JsonConvert.SerializeObject(data);

            return Ok(json);
        }
    }
}