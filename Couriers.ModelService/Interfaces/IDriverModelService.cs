using Couriers.ModelService.Models;
using System.Collections.Generic;

namespace Couriers.ModelService.Interfaces
{
    public interface IDriverModelService
    {
        List<DtoDriver> GetDrivers();
    }
}
