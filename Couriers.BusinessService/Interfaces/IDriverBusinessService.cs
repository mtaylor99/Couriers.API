using Couriers.Database.Models;
using System.Collections.Generic;

namespace Couriers.BusinessService.Interfaces
{
    public interface IDriverBusinessService
    {
        List<Driver> GetDrivers();
    }
}
