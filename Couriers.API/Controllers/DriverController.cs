using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Couriers.ModelService.Interfaces;

namespace Couriers.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController : ControllerBase
    {
        private IDriverModelService _driverModelService;

        public DriverController(IDriverModelService driverModelService)
        {
            _driverModelService = driverModelService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _driverModelService.GetDrivers();

            var json = JsonConvert.SerializeObject(data);

            return Ok(json);
        }
    }
}