using AutoMapper;
using Couriers.BusinessService.Interfaces;
using Couriers.ModelService.Interfaces;
using Couriers.ModelService.Models;
using System.Collections.Generic;

namespace Couriers.ModelService.Domain
{
    public class DriverModelService : IDriverModelService
    {
        private readonly IMapper _mapper;
        private IDriverBusinessService _driverBusinessService;

        public DriverModelService(IMapper mapper, IDriverBusinessService driverBusinessService)
        {
            _mapper = mapper;
            _driverBusinessService = driverBusinessService;
        }

        public List<DtoDriver> GetDrivers()
        {
            var drivers = _driverBusinessService.GetDrivers();
            return _mapper.Map<List<DtoDriver>>(drivers);
        }
    }
}
