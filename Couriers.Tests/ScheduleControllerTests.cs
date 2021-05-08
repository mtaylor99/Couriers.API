using Couriers.API.Controllers;
using Couriers.BusinessService.Domain;
using Couriers.BusinessService.Interfaces;
using Couriers.Database.Interfaces;
using Couriers.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Couriers.ModelService.Models;
using Couriers.ModelService.Interfaces;
using Couriers.ModelService.Domain;
using Couriers.Database.Enums;
using AutoMapper;
using Couriers.ModelService.Mappers;

namespace Couriers.Tests
{
    [TestClass]
    public class ScheduleControllerTests
    {
        private static IMapper _mapper;

        public ScheduleControllerTests()
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
        public void GetSchedules()
        {
            // Arrange
            Mock<IScheduleRepository> mockScheduleRepository = new Mock<IScheduleRepository>();
            Mock<IScheduleItemRepository> mockScheduleItemRepository = new Mock<IScheduleItemRepository>();
            Mock<IScheduleStatusRepository> mockScheduleStatusRepository = new Mock<IScheduleStatusRepository>();
            Mock<IScheduleBusinessService> mockScheduleBusinessService = new Mock<IScheduleBusinessService>();
            Mock<IScheduleModelService> mockScheduleModelService = new Mock<IScheduleModelService>();

            mockScheduleRepository.Setup(m => m.GetSchedules).Returns((new Schedule[] {
                new Schedule { ScheduleId = 1, DriverId = 1,  ScheduleStatus = new ScheduleStatus { Description = "Created" } },
                new Schedule { ScheduleId = 2, DriverId = 1,  ScheduleStatus = new ScheduleStatus { Description = "Created" } },
                new Schedule { ScheduleId = 3, DriverId = 1,  ScheduleStatus = new ScheduleStatus { Description = "Created" } },
                new Schedule { ScheduleId = 4, DriverId = 1,  ScheduleStatus = new ScheduleStatus { Description = "Created" } },
                new Schedule { ScheduleId = 5, DriverId = 1,  ScheduleStatus = new ScheduleStatus { Description = "Created" } },
            }).AsQueryable<Schedule>());

            ScheduleBusinessService businessService = new ScheduleBusinessService(mockScheduleRepository.Object, mockScheduleItemRepository.Object, mockScheduleStatusRepository.Object);
            ScheduleModelService modelService = new ScheduleModelService(_mapper, businessService);
            ScheduleController controller = new ScheduleController(modelService);

            // Act
            OkObjectResult result = controller.Get(string.Empty, 0, 10) as OkObjectResult;
            var schedules = JsonConvert.DeserializeObject<DtoGridResponse<DtoSchedule>>(result.Value.ToString());

            // Assert
            Assert.AreEqual(5, schedules.Count);
            Assert.AreEqual(5, schedules.Result[0].ScheduleId);
            Assert.AreEqual(4, schedules.Result[1].ScheduleId);
            Assert.AreEqual(3, schedules.Result[2].ScheduleId);
            Assert.AreEqual(2, schedules.Result[3].ScheduleId);
            Assert.AreEqual(1, schedules.Result[4].ScheduleId);
        }

        [TestMethod]
        public void GetSchedulesCount()
        {
            // Arrange
            Mock<IScheduleRepository> mockScheduleRepository = new Mock<IScheduleRepository>();
            Mock<IScheduleItemRepository> mockScheduleItemRepository = new Mock<IScheduleItemRepository>();
            Mock<IScheduleStatusRepository> mockScheduleStatusRepository = new Mock<IScheduleStatusRepository>();
            Mock<IScheduleBusinessService> mockScheduleBusinessService = new Mock<IScheduleBusinessService>();
            Mock<IScheduleModelService> mockScheduleModelService = new Mock<IScheduleModelService>();

            mockScheduleRepository.Setup(m => m.GetSchedules).Returns((new Schedule[] {
                new Schedule { ScheduleId = 1, DriverId = 1,  ScheduleStatus = new ScheduleStatus { Description = "Created" } },
                new Schedule { ScheduleId = 2, DriverId = 1,  ScheduleStatus = new ScheduleStatus { Description = "Created" } },
                new Schedule { ScheduleId = 3, DriverId = 1,  ScheduleStatus = new ScheduleStatus { Description = "Created" } },
                new Schedule { ScheduleId = 4, DriverId = 1,  ScheduleStatus = new ScheduleStatus { Description = "Created" } },
                new Schedule { ScheduleId = 5, DriverId = 1,  ScheduleStatus = new ScheduleStatus { Description = "Created" } },
            }).AsQueryable<Schedule>());

            ScheduleBusinessService businessService = new ScheduleBusinessService(mockScheduleRepository.Object, mockScheduleItemRepository.Object, mockScheduleStatusRepository.Object);
            ScheduleModelService modelService = new ScheduleModelService(_mapper, businessService);
            ScheduleController controller = new ScheduleController(modelService);

            // Act
            OkObjectResult result = controller.GetCount() as OkObjectResult;
            var count = JsonConvert.DeserializeObject<int>(result.Value.ToString());

            // Assert
            Assert.IsTrue(count == 5);
        }

        [TestMethod]
        public void GetSchedule()
        {
            // Arrange
            Mock<IScheduleRepository> mockScheduleRepository = new Mock<IScheduleRepository>();
            Mock<IScheduleItemRepository> mockScheduleItemRepository = new Mock<IScheduleItemRepository>();
            Mock<IScheduleStatusRepository> mockScheduleStatusRepository = new Mock<IScheduleStatusRepository>();
            Mock<IScheduleBusinessService> mockScheduleBusinessService = new Mock<IScheduleBusinessService>();
            Mock<IScheduleModelService> mockScheduleModelService = new Mock<IScheduleModelService>();

            mockScheduleRepository.Setup(m => m.GetSchedule(1)).Returns(new Schedule { ScheduleId = 1, DriverId = 1,  ScheduleStatus = new ScheduleStatus { Description = "Created" }});

            ScheduleBusinessService businessService = new ScheduleBusinessService(mockScheduleRepository.Object, mockScheduleItemRepository.Object, mockScheduleStatusRepository.Object);
            ScheduleModelService modelService = new ScheduleModelService(_mapper, businessService);
            ScheduleController controller = new ScheduleController(modelService);

            // Act
            OkObjectResult result = controller.GetSchedule(1) as OkObjectResult;
            var schedule = JsonConvert.DeserializeObject<DtoSchedule>(result.Value.ToString());

            // Assert
            Assert.AreEqual(1, schedule.ScheduleId);
        }

        [TestMethod]
        public void Add()
        {
            // Arrange
            Mock<IScheduleRepository> mockScheduleRepository = new Mock<IScheduleRepository>();
            Mock<IScheduleItemRepository> mockScheduleItemRepository = new Mock<IScheduleItemRepository>();
            Mock<IScheduleStatusRepository> mockScheduleStatusRepository = new Mock<IScheduleStatusRepository>();

            DtoSchedule schedule = new DtoSchedule { ScheduleId = 0, Driver = new DtoDriver { DriverId = 1 }, Date = DateTime.UtcNow, ScheduleStatus = EScheduleStatus.Draft };

            ScheduleBusinessService businessService = new ScheduleBusinessService(mockScheduleRepository.Object, mockScheduleItemRepository.Object, mockScheduleStatusRepository.Object);
            ScheduleModelService modelService = new ScheduleModelService(_mapper, businessService);
            ScheduleController controller = new ScheduleController(modelService);

            // Act
            OkObjectResult result = controller.Post(schedule) as OkObjectResult;
            var scheduleId = JsonConvert.DeserializeObject<int>(result.Value.ToString());

            // Assert
            Assert.IsTrue(scheduleId == schedule.ScheduleId);
        }

        [TestMethod]
        public void Edit()
        {
            // Arrange
            Mock<IScheduleRepository> mockScheduleRepository = new Mock<IScheduleRepository>();
            Mock<IScheduleItemRepository> mockScheduleItemRepository = new Mock<IScheduleItemRepository>();
            Mock<IScheduleStatusRepository> mockScheduleStatusRepository = new Mock<IScheduleStatusRepository>();

            DtoSchedule schedule = new DtoSchedule { ScheduleId = 5, Driver = new DtoDriver { DriverId = 1 }, Date = DateTime.UtcNow, ScheduleStatus = EScheduleStatus.Draft };

            ScheduleBusinessService businessService = new ScheduleBusinessService(mockScheduleRepository.Object, mockScheduleItemRepository.Object, mockScheduleStatusRepository.Object);
            ScheduleModelService modelService = new ScheduleModelService(_mapper, businessService);
            ScheduleController controller = new ScheduleController(modelService);

            // Act
            OkObjectResult result = controller.Put(schedule.ScheduleId, schedule) as OkObjectResult;
            var data = JsonConvert.DeserializeObject<bool>(result.Value.ToString());

            // Assert
            Assert.IsTrue(data);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            Mock<IScheduleRepository> mockScheduleRepository = new Mock<IScheduleRepository>();
            Mock<IScheduleItemRepository> mockScheduleItemRepository = new Mock<IScheduleItemRepository>();
            Mock<IScheduleStatusRepository> mockScheduleStatusRepository = new Mock<IScheduleStatusRepository>();

            ScheduleBusinessService businessService = new ScheduleBusinessService(mockScheduleRepository.Object, mockScheduleItemRepository.Object, mockScheduleStatusRepository.Object);
            ScheduleModelService modelService = new ScheduleModelService(_mapper, businessService);
            ScheduleController controller = new ScheduleController(modelService);

            // Act
            OkObjectResult result = controller.Delete(1) as OkObjectResult;
            var data = JsonConvert.DeserializeObject<bool>(result.Value.ToString());

            // Assert
            Assert.IsTrue(data);
        }

        [TestMethod]
        public void GetScheduleItems()
        {
            // Arrange 
            Mock<IScheduleRepository> mockScheduleRepository = new Mock<IScheduleRepository>();
            Mock<IScheduleItemRepository> mockScheduleItemRepository = new Mock<IScheduleItemRepository>();
            Mock<IScheduleStatusRepository> mockScheduleStatusRepository = new Mock<IScheduleStatusRepository>();
            Mock<IScheduleBusinessService> mockScheduleBusinessService = new Mock<IScheduleBusinessService>();
            Mock<IScheduleModelService> mockScheduleModelService = new Mock<IScheduleModelService>();

            mockScheduleItemRepository.Setup(m => m.GetScheduleItems(1)).Returns((new ScheduleItem[] {
                new ScheduleItem { ScheduleItemId = 1, ScheduleId = 1, JobLineId = 1, DisplayOrder = 1, IsCollection = false, Completed = false },
                new ScheduleItem { ScheduleItemId = 2, ScheduleId = 1, JobLineId = 2, DisplayOrder = 1, IsCollection = false, Completed = false },
                new ScheduleItem { ScheduleItemId = 3, ScheduleId = 2, JobLineId = 3, DisplayOrder = 1, IsCollection = false, Completed = false },
                new ScheduleItem { ScheduleItemId = 4, ScheduleId = 1, JobLineId = 4, DisplayOrder = 1, IsCollection = false, Completed = false },
            }).AsQueryable<ScheduleItem>());

            ScheduleBusinessService businessService = new ScheduleBusinessService(mockScheduleRepository.Object, mockScheduleItemRepository.Object, mockScheduleStatusRepository.Object);
            ScheduleModelService modelService = new ScheduleModelService(_mapper, businessService);
            ScheduleController controller = new ScheduleController(modelService);

            // Act
            OkObjectResult result = controller.GetScheduleItems(1) as OkObjectResult;
            var scheduleItems = JsonConvert.DeserializeObject<List<ScheduleItem>>(result.Value.ToString());

            // Assert
            Assert.AreEqual(1, scheduleItems[0].ScheduleItemId);
            Assert.AreEqual(2, scheduleItems[1].ScheduleItemId);
            Assert.AreEqual(3, scheduleItems[2].ScheduleItemId);
            Assert.AreEqual(4, scheduleItems[3].ScheduleItemId);
        }

        [TestMethod]
        public void UpdateScheduleItems()
        {
            // Arrange
            Mock<IScheduleRepository> mockScheduleRepository = new Mock<IScheduleRepository>();
            Mock<IScheduleItemRepository> mockScheduleItemRepository = new Mock<IScheduleItemRepository>();
            Mock<IScheduleStatusRepository> mockScheduleStatusRepository = new Mock<IScheduleStatusRepository>();

            var scheduleId = 1;
            List<DtoScheduleItem> scheduleItems = new List<DtoScheduleItem> {
                new DtoScheduleItem { ScheduleItemId = 1, DisplayOrder = 1, IsCollection = false, Completed = false },
                new DtoScheduleItem { ScheduleItemId = 2, DisplayOrder = 1, IsCollection = false, Completed = false },
                new DtoScheduleItem { ScheduleItemId = 3, DisplayOrder = 1, IsCollection = false, Completed = false },
                new DtoScheduleItem { ScheduleItemId = 4, DisplayOrder = 1, IsCollection = false, Completed = false },
            };

            ScheduleBusinessService businessService = new ScheduleBusinessService(mockScheduleRepository.Object, mockScheduleItemRepository.Object, mockScheduleStatusRepository.Object);
            ScheduleModelService modelService = new ScheduleModelService(_mapper, businessService);
            ScheduleController controller = new ScheduleController(modelService);

            // Act
            OkObjectResult result = controller.UpdateScheduleItems(scheduleId, scheduleItems) as OkObjectResult;
            var data = JsonConvert.DeserializeObject<bool>(result.Value.ToString());

            // Assert
            Assert.IsTrue(data);
        }

        [TestMethod]
        public void GetStatuses()
        {
            // Arrange 
            Mock<IScheduleRepository> mockScheduleRepository = new Mock<IScheduleRepository>();
            Mock<IScheduleItemRepository> mockScheduleItemRepository = new Mock<IScheduleItemRepository>();
            Mock<IScheduleStatusRepository> mockScheduleStatusRepository = new Mock<IScheduleStatusRepository>();
            Mock<IScheduleBusinessService> mockScheduleBusinessService = new Mock<IScheduleBusinessService>();
            Mock<IScheduleModelService> mockScheduleModelService = new Mock<IScheduleModelService>();

            mockScheduleStatusRepository.Setup(m => m.GetStatuses).Returns((new ScheduleStatus[] {
                new ScheduleStatus { ScheduleStatusId = 1, Description = "Created" },
                new ScheduleStatus { ScheduleStatusId = 2, Description = "Scheduled" },
                new ScheduleStatus { ScheduleStatusId = 3, Description = "On Route" },
                new ScheduleStatus { ScheduleStatusId = 4, Description = "Completed" },
            }).AsQueryable<ScheduleStatus>());

            ScheduleBusinessService businessService = new ScheduleBusinessService(mockScheduleRepository.Object, mockScheduleItemRepository.Object, mockScheduleStatusRepository.Object);
            ScheduleModelService modelService = new ScheduleModelService(_mapper, businessService);
            ScheduleController controller = new ScheduleController(modelService);

            // Act
            OkObjectResult result = controller.GetStatuses() as OkObjectResult;
            var scheduleStatuses = JsonConvert.DeserializeObject<List<ScheduleStatus>>(result.Value.ToString());

            // Assert
            Assert.AreEqual("Created", scheduleStatuses[0].Description);
            Assert.AreEqual("Scheduled", scheduleStatuses[1].Description);
            Assert.AreEqual("On Route", scheduleStatuses[2].Description);
            Assert.AreEqual("Completed", scheduleStatuses[3].Description);
        }
    }
}
