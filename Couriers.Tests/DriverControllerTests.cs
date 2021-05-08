using Couriers.API.Controllers;
using Couriers.BusinessService.Domain;
using Couriers.BusinessService.Interfaces;
using Couriers.Database.Interfaces;
using Couriers.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Couriers.ModelService.Models;
using Couriers.ModelService.Interfaces;
using Couriers.ModelService.Domain;
using AutoMapper;
using Couriers.ModelService.Mappers;

namespace Couriers.Tests
{
    [TestClass]
    public class DriverControllerTests
    {
        private static IMapper _mapper;

        public DriverControllerTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [TestMethod]
        public void GetDrivers()
        {
            // Arrange
            Mock<IDriverRepository> mockDriverRepository = new Mock<IDriverRepository>();
            Mock<IDriverBusinessService> mockDriverBusinessService = new Mock<IDriverBusinessService>();
            Mock<IDriverModelService> mockDriverModelService = new Mock<IDriverModelService>();

            mockDriverRepository.Setup(m => m.GetDrivers).Returns((new Driver[] {
                new Driver { DriverId = 1, FirstName = "FirstName_1", LastName = "LastName_1" },
                new Driver { DriverId = 2, FirstName = "FirstName_2", LastName = "LastName_2" },
                new Driver { DriverId = 3, FirstName = "FirstName_3", LastName = "LastName_3" },
                new Driver { DriverId = 4, FirstName = "FirstName_4", LastName = "LastName_4" },
                new Driver { DriverId = 5, FirstName = "FirstName_5", LastName = "LastName_5" },
            }).AsQueryable<Driver>());

            DriverBusinessService businessService = new DriverBusinessService(mockDriverRepository.Object);
            DriverModelService modelService = new DriverModelService(_mapper, businessService);
            DriverController controller = new DriverController(modelService);

            // Act
            OkObjectResult result = controller.Get() as OkObjectResult;
            var drivers = JsonConvert.DeserializeObject<List<DtoDriver>>(result.Value.ToString());

            // Assert
            Assert.AreEqual("FirstName_1", drivers[0].FirstName);
            Assert.AreEqual("FirstName_2", drivers[1].FirstName);
            Assert.AreEqual("FirstName_3", drivers[2].FirstName);
            Assert.AreEqual("FirstName_4", drivers[3].FirstName);
            Assert.AreEqual("FirstName_5", drivers[4].FirstName);
        }
    }
}
