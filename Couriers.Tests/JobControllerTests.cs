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
    public class JobControllerTests
    {
        private static IMapper _mapper;

        public JobControllerTests()
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
        public void GetJobs()
        {
            // Arrange
            Mock<IJobRepository> mockJobRepository = new Mock<IJobRepository>();
            Mock<IJobStatusRepository> mockJobStatusRepository = new Mock<IJobStatusRepository>();
            Mock<IJobBusinessService> mockJobBusinessService = new Mock<IJobBusinessService>();
            Mock<IJobModelService> mockJobModelService = new Mock<IJobModelService>();

            mockJobRepository.Setup(m => m.GetJobs).Returns((new Job[] {
                new Job { JobId = 1, Customer = "Customer_1", Goods = "Goods_1", OrderedBy= "OrderedBy_1", JobStatus = new JobStatus { Description = "Created" }, CreatedDate = DateTime.UtcNow },
                new Job { JobId = 2, Customer = "Customer_2", Goods = "Goods_2", OrderedBy= "OrderedBy_2", JobStatus = new JobStatus { Description = "Created" }, CreatedDate = DateTime.UtcNow },
                new Job { JobId = 3, Customer = "Customer_3", Goods = "Goods_3", OrderedBy= "OrderedBy_3", JobStatus = new JobStatus { Description = "Created" }, CreatedDate = DateTime.UtcNow },
                new Job { JobId = 4, Customer = "Customer_4", Goods = "Goods_4", OrderedBy= "OrderedBy_4", JobStatus = new JobStatus { Description = "Created" }, CreatedDate = DateTime.UtcNow },
                new Job { JobId = 5, Customer = "Customer_5", Goods = "Goods_5", OrderedBy= "OrderedBy_5", JobStatus = new JobStatus { Description = "Created" }, CreatedDate = DateTime.UtcNow },
            }).AsQueryable<Job>());

            JobBusinessService businessService = new JobBusinessService(mockJobRepository.Object, mockJobStatusRepository.Object);
            JobModelService modelService = new JobModelService(_mapper, businessService);
            JobController controller = new JobController(modelService);

            // Act
            OkObjectResult result = controller.Get(string.Empty, string.Empty, 0, 10) as OkObjectResult;
            var jobs = JsonConvert.DeserializeObject<DtoGridResponse<DtoJob>>(result.Value.ToString());

            // Assert
            Assert.AreEqual(5, jobs.Count);
            Assert.AreEqual("Customer_5", jobs.Result[0].Customer);
            Assert.AreEqual("Customer_4", jobs.Result[1].Customer);
            Assert.AreEqual("Customer_3", jobs.Result[2].Customer);
            Assert.AreEqual("Customer_2", jobs.Result[3].Customer);
            Assert.AreEqual("Customer_1", jobs.Result[4].Customer);
        }

        [TestMethod]
        public void GetJobsCount()
        {
            // Arrange
            Mock<IJobRepository> mockJobRepository = new Mock<IJobRepository>();
            Mock<IJobStatusRepository> mockJobStatusRepository = new Mock<IJobStatusRepository>();
            Mock<IJobBusinessService> mockJobBusinessService = new Mock<IJobBusinessService>();
            Mock<IJobModelService> mockJobModelService = new Mock<IJobModelService>();

            mockJobRepository.Setup(m => m.GetJobs).Returns((new Job[] {
                new Job { JobId = 1, Customer = "Customer_1", Goods = "Goods_1", OrderedBy= "OrderedBy_1", JobStatus = new JobStatus { Description = "Created" }, CreatedDate = DateTime.UtcNow },
                new Job { JobId = 2, Customer = "Customer_2", Goods = "Goods_2", OrderedBy= "OrderedBy_2", JobStatus = new JobStatus { Description = "Created" }, CreatedDate = DateTime.UtcNow },
                new Job { JobId = 3, Customer = "Customer_3", Goods = "Goods_3", OrderedBy= "OrderedBy_3", JobStatus = new JobStatus { Description = "Created" }, CreatedDate = DateTime.UtcNow },
                new Job { JobId = 4, Customer = "Customer_4", Goods = "Goods_4", OrderedBy= "OrderedBy_4", JobStatus = new JobStatus { Description = "Created" }, CreatedDate = DateTime.UtcNow },
                new Job { JobId = 5, Customer = "Customer_5", Goods = "Goods_5", OrderedBy= "OrderedBy_5", JobStatus = new JobStatus { Description = "Created" }, CreatedDate = DateTime.UtcNow },
            }).AsQueryable<Job>());

            JobBusinessService businessService = new JobBusinessService(mockJobRepository.Object, mockJobStatusRepository.Object);
            JobModelService modelService = new JobModelService(_mapper, businessService);
            JobController controller = new JobController(modelService);

            // Act
            OkObjectResult result = controller.GetCount(string.Empty) as OkObjectResult;
            var count = JsonConvert.DeserializeObject<int>(result.Value.ToString());

            // Assert
            Assert.IsTrue(count == 5);
        }

        [TestMethod]
        public void GetJob()
        {
            // Arrange
            Mock<IJobRepository> mockJobRepository = new Mock<IJobRepository>();
            Mock<IJobStatusRepository> mockJobStatusRepository = new Mock<IJobStatusRepository>();
            Mock<IJobBusinessService> mockJobBusinessService = new Mock<IJobBusinessService>();
            Mock<IJobModelService> mockJobModelService = new Mock<IJobModelService>();

            mockJobRepository.Setup(m => m.GetJob(1)).Returns(new Job { JobId = 1, Customer = "Customer_1", Goods = "Goods_1", OrderedBy= "OrderedBy_1", JobStatus = new JobStatus { Description = "Created" }, CreatedDate = DateTime.UtcNow });

            JobBusinessService businessService = new JobBusinessService(mockJobRepository.Object, mockJobStatusRepository.Object);
            JobModelService modelService = new JobModelService(_mapper, businessService);
            JobController controller = new JobController(modelService);

            // Act
            OkObjectResult result = controller.GetJob(1) as OkObjectResult;
            var job = JsonConvert.DeserializeObject<DtoJob>(result.Value.ToString());

            // Assert
            Assert.AreEqual(1, job.JobId);
        }

        [TestMethod]
        public void Add()
        {
            // Arrange
            Mock<IJobRepository> mockJobRepository = new Mock<IJobRepository>();
            Mock<IJobStatusRepository> mockJobStatusRepository = new Mock<IJobStatusRepository>();

            DtoJob job = new DtoJob { Customer = "Customer_6", Goods = "Goods_6", OrderedBy = "OrderedBy_6", JobStatus = EJobStatus.Created, CreatedDate = DateTime.UtcNow };

            JobBusinessService businessService = new JobBusinessService(mockJobRepository.Object, mockJobStatusRepository.Object);
            JobModelService modelService = new JobModelService(_mapper, businessService);
            JobController controller = new JobController(modelService);

            // Act
            OkObjectResult result = controller.Post(job) as OkObjectResult;
            var jobId = JsonConvert.DeserializeObject<int>(result.Value.ToString());

            // Assert
            Assert.IsTrue(jobId == job.JobId);
        }

        [TestMethod]
        public void Edit()
        {
            // Arrange
            Mock<IJobRepository> mockJobRepository = new Mock<IJobRepository>();
            Mock<IJobStatusRepository> mockJobStatusRepository = new Mock<IJobStatusRepository>();

            DtoJob job = new DtoJob { JobId = 5, Customer = "Customer_5_1", Goods = "Goods_5_1", OrderedBy = "OrderedBy_5_1", JobStatus = EJobStatus.Created, CreatedDate = DateTime.UtcNow };

            JobBusinessService businessService = new JobBusinessService(mockJobRepository.Object, mockJobStatusRepository.Object);
            JobModelService modelService = new JobModelService(_mapper, businessService);
            JobController controller = new JobController(modelService);

            // Act
            OkObjectResult result = controller.Put(job.JobId, job) as OkObjectResult;
            var data = JsonConvert.DeserializeObject<bool>(result.Value.ToString());

            // Assert
            Assert.IsTrue(data);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            Mock<IJobRepository> mockJobRepository = new Mock<IJobRepository>();
            Mock<IJobStatusRepository> mockJobStatusRepository = new Mock<IJobStatusRepository>();

            JobBusinessService businessService = new JobBusinessService(mockJobRepository.Object, mockJobStatusRepository.Object);
            JobModelService modelService = new JobModelService(_mapper, businessService);
            JobController controller = new JobController(modelService);

            // Act
            OkObjectResult result = controller.Delete(1) as OkObjectResult;
            var data = JsonConvert.DeserializeObject<bool>(result.Value.ToString());

            // Assert
            Assert.IsTrue(data);
        }

        [TestMethod]
        public void GetStatuses()
        {
            // Arrange 
            Mock<IJobRepository> mockJobRepository = new Mock<IJobRepository>();
            Mock<IJobStatusRepository> mockJobStatusRepository = new Mock<IJobStatusRepository>();
            Mock<IJobBusinessService> mockJobBusinessService = new Mock<IJobBusinessService>();
            Mock<IJobModelService> mockJobModelService = new Mock<IJobModelService>();

            mockJobStatusRepository.Setup(m => m.GetStatuses).Returns((new JobStatus[] {
                new JobStatus { JobStatusId = 1, Description = "Created" },
                new JobStatus { JobStatusId = 2, Description = "Scheduled" },
                new JobStatus { JobStatusId = 3, Description = "Completed" },
            }).AsQueryable<JobStatus>());

            JobBusinessService businessService = new JobBusinessService(mockJobRepository.Object, mockJobStatusRepository.Object);
            JobModelService modelService = new JobModelService(_mapper, businessService);
            JobController controller = new JobController(modelService);

            // Act
            OkObjectResult result = controller.GetStatuses() as OkObjectResult;
            var jobStatuses = JsonConvert.DeserializeObject<List<JobStatus>>(result.Value.ToString());

            // Assert
            Assert.AreEqual("Created", jobStatuses[0].Description);
            Assert.AreEqual("Scheduled", jobStatuses[1].Description);
            Assert.AreEqual("Completed", jobStatuses[2].Description);
        }
    }
}
