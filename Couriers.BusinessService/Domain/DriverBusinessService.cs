using Couriers.BusinessService.Interfaces;
using Couriers.Database.Interfaces;
using Couriers.Database.Models;
using System.Collections.Generic;
using System.Linq;

namespace Couriers.BusinessService.Domain
{
    public class DriverBusinessService : IDriverBusinessService
    {
        private IDriverRepository _driverRepository;

        public DriverBusinessService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public List<Driver> GetDrivers()
        {
            return _driverRepository.GetDrivers.ToList();
        }
    }
}
